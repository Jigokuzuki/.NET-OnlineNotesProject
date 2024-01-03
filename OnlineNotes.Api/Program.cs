using OnlineNotes.Api.Entities;

const string GetNoteEndpointName = "GetNote";

List<Note> notes = new()
{
    new Note()
    {
        Id = 1,
        Title = "Test1",
        Content = "Testing notes",
        CreatedDate = new DateTimeOffset(2020, 12, 23, 22, 11, 0, TimeSpan.FromHours(1)),
        ModifiedDate = new DateTimeOffset(2023, 12, 23, 22, 11, 0, TimeSpan.FromHours(1)),
        Category = "School"
    },

        new Note()
    {
        Id = 2,
        Title = "Test2sss",
        Content = "Testing notes part2",
        CreatedDate = new DateTimeOffset(2020, 1, 1, 1, 1, 0, TimeSpan.FromHours(1)),
        ModifiedDate = new DateTimeOffset(2023, 11, 11, 11, 11, 0, TimeSpan.FromHours(1)),
        Category = "Home"
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/notes", () => notes);

app.MapGet("/notes/{id}", (int id) =>
{
    Note? note = notes.Find(game => game.Id == id);

    if (note is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(note);
})
.WithName(GetNoteEndpointName);

app.MapPost("/notes", (Note note) =>
{
    note.Id = notes.Max(note => note.Id) + 1;
    notes.Add(note);

    return Results.CreatedAtRoute(GetNoteEndpointName, new { id = note.Id }, note);
});

app.MapPut("/notes/{id}", (int id, Note updatedNote) =>
{
    Note? existingNote = notes.Find(note => note.Id == id);

    if (existingNote is null)
    {
        return Results.NotFound();
    }


    existingNote.Title = updatedNote.Title;
    existingNote.Content = updatedNote.Content;
    existingNote.Category = updatedNote.Category;


    existingNote.ModifiedDate = DateTime.Now;
    existingNote.ModifiedDate = existingNote.ModifiedDate.AddTicks(-(existingNote.ModifiedDate.Ticks % TimeSpan.TicksPerSecond));


    return Results.NoContent();
});



app.MapDelete("/notes/{id}", (int id) =>
{
    Note? note = notes.Find(note => note.Id == id);

    if (note is not null)
    {
        notes.Remove(note);
    }


    return Results.NoContent();
});



app.Run();
