using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Repositories;

public class InMemNotesRepository
{
    private readonly List<Note> notes = new()
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

    public IEnumerable<Note> GetAll()
    {
        return notes;
    }

    public Note? Get(int id)
    {
        return notes.Find(note => note.Id == id);
    }

    public void Create(Note note)
    {
        note.Id = notes.Max(note => note.Id) + 1;
        notes.Add(note);
    }

    public void Update(Note updatedNote)
    {
        var index = notes.FindIndex(note => note.Id == updatedNote.Id);
        notes[index] = updatedNote;
    }

    public void Delete(int id)
    {
        var index = notes.FindIndex(note => note.Id == id);
        notes.RemoveAt(index);
    }
}