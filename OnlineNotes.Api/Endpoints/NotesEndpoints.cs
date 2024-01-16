using OnlineNotes.Api.Dtos;
using OnlineNotes.Api.Entities;
using OnlineNotes.Api.Repositories;

namespace OnlineNotes.Api.Endpoints;

public static class NotesEndpoints
{
    const string GetNoteEndpointName = "GetNote";

    public static RouteGroupBuilder MapNotesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/notes").WithParameterValidation();

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

        group.MapGet("/user/{userId}", async (INotesRepository repository, int userId) =>
        {
            var userNotes = await repository.GetNotesByUserIdAsync(userId);

            if (userNotes == null || !userNotes.Any())
            {
                return Results.NotFound();
            }

            return Results.Ok(userNotes.Select(note => note.AsDto()));
        });


        group.MapPost("/", async (INotesRepository repository, CreateNoteDto noteDto) =>
        {
            if (string.IsNullOrEmpty(noteDto.Title))
            {
                return Results.BadRequest(new { message = "Title cannot be empty!" });
            }

            if (string.IsNullOrEmpty(noteDto.Content))
            {
                return Results.BadRequest(new { message = "Content cannot be empty!" });
            }

            if (string.IsNullOrEmpty(noteDto.Category))
            {
                return Results.BadRequest(new { message = "Category cannot be empty!" });
            }


            Note note = new()
            {
                Title = noteDto.Title,
                Content = noteDto.Content,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now,
                Category = noteDto.Category,
                IsFavorite = noteDto.IsFavorite,
                Color = noteDto.Color

            };
            note.CreatedDate = note.CreatedDate.AddTicks(-(note.CreatedDate.Ticks % TimeSpan.TicksPerSecond));
            note.ModifiedDate = note.ModifiedDate.AddTicks(-(note.ModifiedDate.Ticks % TimeSpan.TicksPerSecond));
            await repository.CreateAsync(note);

            return Results.CreatedAtRoute(GetNoteEndpointName, new { id = note.Id }, note);
        });

        group.MapPut("/{id}", async (INotesRepository repository, int id, UpdateNoteDto updateNoteDto) =>
        {
            Note? existingNote = await repository.GetAsync(id);

            if (existingNote is null)
            {
                return Results.NotFound();
            }

            if (string.IsNullOrEmpty(updateNoteDto.Title))
            {
                return Results.BadRequest(new { message = "Title cannot be empty!" });
            }

            if (string.IsNullOrEmpty(updateNoteDto.Content))
            {
                return Results.BadRequest(new { message = "Content cannot be empty!" });
            }

            if (string.IsNullOrEmpty(updateNoteDto.Category))
            {
                return Results.BadRequest(new { message = "Category cannot be empty!" });
            }


            existingNote.Title = updateNoteDto.Title;
            existingNote.Content = updateNoteDto.Content;
            existingNote.Category = updateNoteDto.Category;
            existingNote.ModifiedDate = DateTime.Now;
            existingNote.ModifiedDate = existingNote.ModifiedDate.AddTicks(-(existingNote.ModifiedDate.Ticks % TimeSpan.TicksPerSecond));
            existingNote.IsFavorite = updateNoteDto.IsFavorite;
            existingNote.Color = updateNoteDto.Color;


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