using OnlineNotes.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapNotesEndpoints();

app.Run();
