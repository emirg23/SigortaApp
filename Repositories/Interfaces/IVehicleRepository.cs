using SigortaApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SigortaApp.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllAsync();
        Task<Vehicle?> GetByIdAsync(int id);
        Task AddAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
        Task SaveChangesAsync();
    }
}
