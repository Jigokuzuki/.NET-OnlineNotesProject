using OnlineNotes.Api.Entities;

namespace OnlineNotes.Api.Repositories;

public interface IUsersRepository
{
    Task CreateAsync(User user);
    Task DeleteAsync(int id);
    Task<User?> GetAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task UpdateAsync(User updatedUser);
}
