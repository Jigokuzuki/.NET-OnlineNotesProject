using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Repositories
{
    public interface IUserNotesRepository
    {
        Task CreateAsync(UserNotes userNotes);
        Task DeleteAsync(int id);
        Task<UserNotes?> GetAsync(int id);
        Task<IEnumerable<UserNotes>> GetAllAsync();
        Task<IEnumerable<UserNotes>> GetByUserIdAsync(int userId);
        Task UpdateAsync(UserNotes updatedUserNotes);
        Task DeleteByNoteAndUserAsync(int noteId, int userId);
    }
}
