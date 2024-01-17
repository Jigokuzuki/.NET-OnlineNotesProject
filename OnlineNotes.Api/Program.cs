using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;
using OnlineNotes.Api.Authorization;
using OnlineNotes.Api.Data;
using OnlineNotes.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepo(builder.Configuration);
builder.Services.AddRepo2(builder.Configuration);
builder.Services.AddRepo3(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JwtKey123456789012345678901234567890")),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudiences = new[] { "http://localhost:46707", "https://localhost:44362", "http://localhost:5209", "https://localhost:7076" },
                    ValidIssuers = new[] { "dotnet-user-jwts" },
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });


builder.Services.AddRazorPages();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });

var app = builder.Build();

await app.Services.InitalizeDbAsync();

app.UseCors("AllowAllOrigins");



app.MapNotesEndpoints();
app.MapUsersEndpoints();
app.MapUserNotesEndpoints();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
