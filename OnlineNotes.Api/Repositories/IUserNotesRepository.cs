using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Repositories;

public interface IUserNotesRepository
{
    Task CreateAsync(UserNotes userNotes);
    Task DeleteAsync(int id);
    Task<UserNotes?> GetAsync(int id);
    Task<IEnumerable<UserNotes>> GetAllAsync();
    Task UpdateAsync(UserNotes updatedUserNotes);
}
