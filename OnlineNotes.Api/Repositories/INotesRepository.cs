using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Repositories;

public interface INotesRepository
{
    Task CreateAsync(Note note);
    Task DeleteAsync(int id);
    Task<Note?> GetAsync(int id);
    Task<IEnumerable<Note>> GetAllAsync();
    Task UpdateAsync(Note updatedNote);
}
