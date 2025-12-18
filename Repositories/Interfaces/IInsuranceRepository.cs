using SigortaApp.Models;

namespace SigortaApp.Repositories.Interfaces
{
    public interface IInsuranceRepository
    {
        Task<List<Insurance>> GetAllAsync(int? id, string? companyName);
        Task<List<Insurance>> GetExpiredAsync();
        Task<Insurance?> GetByIdAsync(int id);
        Task AddAsync(Insurance insurance);
        Task DeleteAsync(Insurance insurance);
        Task SaveChangesAsync();
    }
}
