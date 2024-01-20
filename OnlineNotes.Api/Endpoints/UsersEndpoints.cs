using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OnlineNotes.Api.Authorization;
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
            (await repository.GetAllAsync()).Select(user => user.AsDto()));

        group.MapGet("/{id}", async (IUsersRepository repository, int id) =>
        {
            User? user = await repository.GetAsync(id);

            if (user is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user.AsDto());
        })
        .WithName(GetUserEndpointName);

        group.MapPost("/", async (IUsersRepository repository, CreateUserDto userDto) =>
        {

            var existingUser = await repository.GetUserByEmail(userDto.Email);

            if (existingUser != null)
            {
                return Results.Conflict(new { message = "User already exists!" });
            }

            if (string.IsNullOrEmpty(userDto.FirstName))
            {
                return Results.Conflict(new { message = "Invalid First Name!" });
            }

            if (string.IsNullOrEmpty(userDto.Surname))
            {
                return Results.Conflict(new { message = "Invalid Surname!" });
            }

            if (string.IsNullOrEmpty(userDto.Email) || !userDto.Email.Contains("@"))
            {
                return Results.Conflict(new { message = "Invalid Email!" });
            }

            if (string.IsNullOrEmpty(userDto.Password) || userDto.Password.Length < 8 || !userDto.Password.Any(c => !char.IsLetterOrDigit(c)))
            {
                return Results.Conflict(new { message = "Invalid Password! Password must be at least 8 characters long and contain at least one special character." });
            }

            User user = new()
            {
                FirstName = userDto.FirstName,
                Surname = userDto.Surname,
                Email = userDto.Email,
                Password = userDto.Password,
                RegisterDate = DateTimeOffset.Now
            };

            user.RegisterDate = user.RegisterDate.AddTicks(-(user.RegisterDate.Ticks % TimeSpan.TicksPerSecond));
            await repository.CreateAsync(user);

            //token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("JwtKey123456789012345678901234567890");

            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.Name, user.Id.ToString()),
              new Claim("aud", "http://localhost:46707"),
              new Claim("aud", "https://localhost:44362"),
              new Claim("aud", "http://localhost:5209"),
              new Claim("aud", "https://localhost:7076"),
              new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
              new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddHours(1)).ToUnixTimeSeconds().ToString()),
              new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer),
              new Claim(JwtRegisteredClaimNames.Iss, "dotnet-user-jwts")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);



            return Results.Ok(new { id = user.Id, token = tokenString });
        });//.RequireAuthorization(Policies.WriteAccess);

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


            await repository.UpdateAsync(existingUser);

            return Results.NoContent();
        }).RequireAuthorization(Policies.WriteAccess);


        group.MapDelete("/{id}", async (IUsersRepository repository, int id) =>
        {
            User? user = await repository.GetAsync(id);

            if (user is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        }).RequireAuthorization(Policies.WriteAccess);


        group.MapPost("/login", async (IUsersRepository repository, LoginUserDto userDto) =>
        {
            var user = await repository.GetUserByEmail(userDto.Email);

            if (user is null)
            {
                return Results.NotFound(new { message = "User not found" });
            }

            if (user.Password != userDto.Password)
            {
                return Results.Conflict(new { message = "Invalid password!" });
            }

            if (string.IsNullOrEmpty(userDto.Email) || !userDto.Email.Contains("@"))
            {
                return Results.Conflict(new { message = "Invalid Email!" });
            }

            if (user != null && user.Password == userDto.Password)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                // hide key
                var key = Encoding.UTF8.GetBytes("JwtKey123456789012345678901234567890");

                var claims = new List<Claim>
                {
                  new Claim(ClaimTypes.Name, user.Id.ToString()),
                  new Claim("aud", "http://localhost:46707"),
                  new Claim("aud", "https://localhost:44362"),
                  new Claim("aud", "http://localhost:5209"),
                  new Claim("aud", "https://localhost:7076"),
                  new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                  new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddHours(1)).ToUnixTimeSeconds().ToString()),
                  new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer),
                  new Claim(JwtRegisteredClaimNames.Iss, "dotnet-user-jwts")
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Results.Ok(new { id = user.Id, token = tokenString });
            }

            return Results.BadRequest();
        });

        //.RequireAuthorization(Policies.WriteAccess);

        return group;
    }

}