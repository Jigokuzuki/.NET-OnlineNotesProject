using OnlineNotes.Api.Authorization;
using OnlineNotes.Api.Data;
using OnlineNotes.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepo(builder.Configuration);
builder.Services.AddRepo2(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy(Policies.ReadAccess, builder => builder.RequireClaim("scope", "read"));
    option.AddPolicy(Policies.WriteAccess, builder => builder.RequireClaim("scope", "write").RequireRole("Admin"));

});

var app = builder.Build();

await app.Services.InitalizeDbAsync();

app.MapNotesEndpoints();
app.MapUsersEndpoints();

app.Run();
