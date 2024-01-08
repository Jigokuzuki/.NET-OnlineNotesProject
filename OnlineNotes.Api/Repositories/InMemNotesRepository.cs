using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Repositories;

public class InMemNotesRepository : INotesRepository
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

    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        return await Task.FromResult(notes);
    }

    public async Task<Note?> GetAsync(int id)
    {
        return await Task.FromResult(notes.Find(note => note.Id == id));
    }

    public async Task CreateAsync(Note note)
    {
        note.Id = notes.Max(note => note.Id) + 1;
        notes.Add(note);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Note updatedNote)
    {
        var index = notes.FindIndex(note => note.Id == updatedNote.Id);
        notes[index] = updatedNote;

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = notes.FindIndex(note => note.Id == id);
        notes.RemoveAt(index);

        await Task.CompletedTask;
    }
}