using OnlineNotes.Api.Data;
using OnlineNotes.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepo(builder.Configuration);
builder.Services.AddRepo2(builder.Configuration);
builder.Services.AddAuthentication();

var app = builder.Build();

await app.Services.InitalizeDbAsync();

app.MapNotesEndpoints();
app.MapUsersEndpoints();

app.Run();
