using OnlineNotes.Api.Dtos;
using OnlineNotes.Api.Entities;
using OnlineNotes.Api.Repositories;

namespace OnlineNotes.Api.Endpoints;

public static class UserNotesEndpoints
{
    const string GetUserNotesEndpointName = "GetUserNotes";

    public static RouteGroupBuilder MapUserNotesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/usernotes").WithParameterValidation();

        group.MapGet("/", async (IUserNotesRepository repository) =>
            (await repository.GetAllAsync()).Select(userNotes => userNotes.AsDto()));

        group.MapGet("/{id}", async (IUserNotesRepository repository, int id) =>
        {
            UserNotes? userNotes = await repository.GetAsync(id);

            if (userNotes is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(userNotes.AsDto());
        })
        .WithName(GetUserNotesEndpointName);

        group.MapGet("/users/{userId}", async (HttpContext context, IUserNotesRepository repository, int userId) =>
        {
            var userNotes = await repository.GetByUserIdAsync(userId);

            if (userNotes != null)
            {
                await context.Response.WriteAsJsonAsync(userNotes);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        });

        group.MapPost("/", async (IUserNotesRepository repository, CreateUserNotesDto userNotesDto) =>
        {
            UserNotes userNotes = new()
            {
                UserId = userNotesDto.UserId,
                NoteId = userNotesDto.NoteId
            };
            await repository.CreateAsync(userNotes);

            return Results.CreatedAtRoute(GetUserNotesEndpointName, new { id = userNotes.Id }, userNotes);
        });

        group.MapPut("/{id}", async (IUserNotesRepository repository, int id, UpdateUserNotesDto updateUserNoteDto) =>
        {
            UserNotes? existingUserNote = await repository.GetAsync(id);

            if (existingUserNote is null)
            {
                return Results.NotFound();
            }

            existingUserNote.UserId = updateUserNoteDto.UserId;
            existingUserNote.NoteId = updateUserNoteDto.NoteId;

            await repository.UpdateAsync(existingUserNote);

            return Results.NoContent();
        });


        group.MapDelete("/{id}", async (IUserNotesRepository repository, int id) =>
        {
            UserNotes? userNotes = await repository.GetAsync(id);

            if (userNotes is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}