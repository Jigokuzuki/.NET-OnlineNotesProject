using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        return await dbContext.Notes.AsNoTracking().ToListAsync();
    }

    public async Task<Note?> GetAsync(int id)
    {
        return await dbContext.Notes.FindAsync(id);
    }

    public async Task CreateAsync(Note note)
    {
        dbContext.Notes.Add(note);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Note updatedNote)
    {
        dbContext.Update(updatedNote);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.Notes.Where(note => note.Id == id).ExecuteDeleteAsync();
    }
}