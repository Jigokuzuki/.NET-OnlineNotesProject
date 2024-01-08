using Microsoft.EntityFrameworkCore;
using OnlineNotes.Api.Data;
using OnlineNotes.Api.Endpoints;
using OnlineNotes.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<INotesRepository, InMemNotesRepository>();

var conString = builder.Configuration.GetConnectionString("OnlineNotesContext");
builder.Services.AddSqlServer<OnlineNotesContext>(conString);

var app = builder.Build();

app.Services.InitalizeDb();

app.MapNotesEndpoints();

app.Run();
