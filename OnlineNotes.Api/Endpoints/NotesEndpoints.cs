using OnlineNotes.Api.Entities;
using OnlineNotes.Api.Repositories;

namespace OnlineNotes.Api.Endpoints;

public static class NotesEndpoints
{
    const string GetNoteEndpointName = "GetNote";

    public static RouteGroupBuilder MapNotesEndpoints(this IEndpointRouteBuilder routes)
    {
        InMemNotesRepository repository = new();

        var group = routes.MapGroup("/notes")
                        .WithParameterValidation();


        group.MapGet("/", () => repository.GetAll());

        group.MapGet("/{id}", (int id) =>
        {
            Note? note = repository.Get(id);

            if (note is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(note);
        })
        .WithName(GetNoteEndpointName);

        group.MapPost("/", (Note note) =>
        {
            repository.Create(note);

            return Results.CreatedAtRoute(GetNoteEndpointName, new { id = note.Id }, note);
        });

        group.MapPut("/{id}", (int id, Note updatedNote) =>
        {
            Note? existingNote = repository.Get(id);

            if (existingNote is null)
            {
                return Results.NotFound();
            }


            existingNote.Title = updatedNote.Title;
            existingNote.Content = updatedNote.Content;
            existingNote.Category = updatedNote.Category;


            existingNote.ModifiedDate = DateTime.Now;
            existingNote.ModifiedDate = existingNote.ModifiedDate.AddTicks(-(existingNote.ModifiedDate.Ticks % TimeSpan.TicksPerSecond));

            repository.Update(existingNote);

            return Results.NoContent();
        });



        group.MapDelete("/{id}", (int id) =>
        {
            Note? note = repository.Get(id);

            if (note is not null)
            {
                repository.Delete(id);
            }


            return Results.NoContent();
        });

        return group;
    }
}