using SigortaApp.Models;

namespace SigortaApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
        Task<User?> GetByEmailAsync(string email);
    }
}