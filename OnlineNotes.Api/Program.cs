using OnlineNotes.Api.Data;
using OnlineNotes.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepo(builder.Configuration);

var app = builder.Build();

app.Services.InitalizeDb();

app.MapNotesEndpoints();

app.Run();
