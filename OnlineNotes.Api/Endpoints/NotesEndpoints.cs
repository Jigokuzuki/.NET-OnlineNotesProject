using OnlineNotes.Api.Entities;
using OnlineNotes.Api.Repositories;

namespace OnlineNotes.Api.Endpoints;

public static class NotesEndpoints
{
    const string GetNoteEndpointName = "GetNote";

    public static RouteGroupBuilder MapNotesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/notes")
                        .WithParameterValidation();


        group.MapGet("/", (INotesRepository repository) => repository.GetAll());

        group.MapGet("/{id}", (INotesRepository repository, int id) =>
        {
            Note? note = repository.Get(id);

            if (note is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(note);
        })
        .WithName(GetNoteEndpointName);

        group.MapPost("/", (INotesRepository repository, Note note) =>
        {
            repository.Create(note);

            return Results.CreatedAtRoute(GetNoteEndpointName, new { id = note.Id }, note);
        });

        group.MapPut("/{id}", (INotesRepository repository, int id, Note updatedNote) =>
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



        group.MapDelete("/{id}", (INotesRepository repository, int id) =>
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