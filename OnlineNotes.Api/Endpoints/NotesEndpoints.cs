using OnlineNotes.Api.Dtos;
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

        group.MapGet("/", async (INotesRepository repository) =>
            (await repository.GetAllAsync()).Select(note => note.AsDto()));

        group.MapGet("/{id}", async (INotesRepository repository, int id) =>
        {
            Note? note = await repository.GetAsync(id);

            if (note is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(note.AsDto());
        })
        .WithName(GetNoteEndpointName);

        group.MapPost("/", async (INotesRepository repository, CreateNoteDto noteDto) =>
        {
            Note note = new()
            {
                Title = noteDto.Title,
                Content = noteDto.Content,
                CreatedDate = noteDto.CreatedDate,
                ModifiedDate = DateTimeOffset.Now,
                Category = noteDto.Category,
                IsFavorite = noteDto.IsFavorite,
                Color = noteDto.Color

            };
            note.ModifiedDate = note.ModifiedDate.AddTicks(-(note.ModifiedDate.Ticks % TimeSpan.TicksPerSecond));
            await repository.CreateAsync(note);

            return Results.CreatedAtRoute(GetNoteEndpointName, new { id = note.Id }, note);
        });

        group.MapPut("/{id}", async (INotesRepository repository, int id, UpdateNoteDto updatedNoteDto) =>
        {
            Note? existingNote = await repository.GetAsync(id);

            if (existingNote is null)
            {
                return Results.NotFound();
            }


            existingNote.Title = updatedNoteDto.Title;
            existingNote.Content = updatedNoteDto.Content;
            existingNote.Category = updatedNoteDto.Category;
            existingNote.ModifiedDate = DateTime.Now;
            existingNote.ModifiedDate = existingNote.ModifiedDate.AddTicks(-(existingNote.ModifiedDate.Ticks % TimeSpan.TicksPerSecond));
            existingNote.IsFavorite = updatedNoteDto.IsFavorite;
            existingNote.Color = updatedNoteDto.Color;


            await repository.UpdateAsync(existingNote);

            return Results.NoContent();
        });


        group.MapDelete("/{id}", async (INotesRepository repository, int id) =>
        {
            Note? note = await repository.GetAsync(id);

            if (note is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}