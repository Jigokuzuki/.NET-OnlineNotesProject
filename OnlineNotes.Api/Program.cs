using OnlineNotes.Api.Entities;



List<Note> notes = new()
{
    new Note()
    {
        Id = 1,
        Title = "Test1",
        Content = "Testing notes",
        CreatedDate = new DateTimeOffset(2023, 12, 23, 22, 11, 0, TimeSpan.FromHours(1)),
        ModifiedDate = new DateTimeOffset(DateTime.Now),
        Category = "School"
    },

        new Note()
    {
        Id = 2,
        Title = "Test2",
        Content = "Testing notes part2",
        CreatedDate = new DateTimeOffset(2022, 1, 1, 1, 1, 0, TimeSpan.FromHours(1)),
        ModifiedDate = new DateTimeOffset(DateTime.Now),
        Category = "Home"
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/notes", () => notes);

app.MapGet("/notes/{id}", (int id) => notes.Find(game => game.Id == id));

app.Run();
