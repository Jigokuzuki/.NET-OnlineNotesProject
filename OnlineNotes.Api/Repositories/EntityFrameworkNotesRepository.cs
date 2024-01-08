using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OnlineNotes.Api.Data;
using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Repositories;

public class EntityFrameworkNotesRepository : INotesRepository
{
    private readonly OnlineNotesContext dbContext;

    public EntityFrameworkNotesRepository(OnlineNotesContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<Note> GetAll()
    {
        return dbContext.Notes.AsNoTracking().ToList();
    }

    public Note? Get(int id)
    {
        return dbContext.Notes.Find(id);
    }

    public void Create(Note note)
    {
        dbContext.Notes.Add(note);
        dbContext.SaveChanges();
    }

    public void Update(Note updatedNote)
    {
        dbContext.Update(updatedNote);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        dbContext.Notes.Where(note => note.Id == id).ExecuteDelete();
    }
}