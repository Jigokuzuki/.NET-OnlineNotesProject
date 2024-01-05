using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Repositories;

public interface INotesRepository
{
    void Create(Note note);
    void Delete(int id);
    Note? Get(int id);
    IEnumerable<Note> GetAll();
    void Update(Note updatedNote);
}
