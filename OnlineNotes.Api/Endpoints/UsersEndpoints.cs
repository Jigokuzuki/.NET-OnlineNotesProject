using OnlineNotes.Api.Dtos;
using OnlineNotes.Api.Entities;
using OnlineNotes.Api.Repositories;

namespace OnlineNotes.Api.Endpoints;

public static class UsersEndpoints
{
    const string GetUserEndpointName = "GetUser";

    public static RouteGroupBuilder MapUsersEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users").WithParameterValidation();

        group.MapGet("/", async (IUsersRepository repository) =>
            (await repository.GetAllAsync()).Select(user => user.AsDto())).RequireAuthorization();

        group.MapGet("/{id}", async (IUsersRepository repository, int id) =>
        {
            User? user = await repository.GetAsync(id);

            if (user is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user.AsDto());
        })
        .WithName(GetUserEndpointName).RequireAuthorization();

        group.MapPost("/", async (IUsersRepository repository, CreateUserDto userDto) =>
        {
            User user = new()
            {
                FirstName = userDto.FirstName,
                Surname = userDto.Surname,
                Email = userDto.Email,
                Password = userDto.Password,
                Avatar = userDto.Avatar,
                RegisterDate = DateTimeOffset.Now
            };

            user.RegisterDate = user.RegisterDate.AddTicks(-(user.RegisterDate.Ticks % TimeSpan.TicksPerSecond));
            await repository.CreateAsync(user);

            return Results.CreatedAtRoute(GetUserEndpointName, new { id = user.Id }, user);
        }).RequireAuthorization();

        group.MapPut("/{id}", async (IUsersRepository repository, int id, UpdateUserDto updateUserDto) =>
        {
            User? existingUser = await repository.GetAsync(id);

            if (existingUser is null)
            {
                return Results.NotFound();
            }


            existingUser.FirstName = updateUserDto.FirstName;
            existingUser.Surname = updateUserDto.Surname;
            existingUser.Email = updateUserDto.Email;
            existingUser.Password = updateUserDto.Password;
            existingUser.Avatar = updateUserDto.Avatar;


            await repository.UpdateAsync(existingUser);

            return Results.NoContent();
        }).RequireAuthorization();


        group.MapDelete("/{id}", async (IUsersRepository repository, int id) =>
        {
            User? user = await repository.GetAsync(id);

            if (user is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        }).RequireAuthorization();

        return group;
    }
}