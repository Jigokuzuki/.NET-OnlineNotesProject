using Microsoft.EntityFrameworkCore;
using OnlineNotes.Api.Data;
using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Repositories;

public class EntityFrameworkUserNotesRepository : IUserNotesRepository
{
    private readonly OnlineNotesContext dbContext;

    public EntityFrameworkUserNotesRepository(OnlineNotesContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<UserNotes>> GetAllAsync()
    {
        return await dbContext.UserNotes.AsNoTracking().ToListAsync();
    }

    public async Task<UserNotes?> GetAsync(int id)
    {
        return await dbContext.UserNotes.FindAsync(id);
    }

    public async Task CreateAsync(UserNotes userNotes)
    {
        dbContext.UserNotes.Add(userNotes);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserNotes updatedUserNotes)
    {
        dbContext.Update(updatedUserNotes);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.UserNotes.Where(userNotes => userNotes.Id == id).ExecuteDeleteAsync();
    }
}