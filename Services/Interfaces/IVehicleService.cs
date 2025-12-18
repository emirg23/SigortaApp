using Sigorta.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SigortaApp.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<object>> GetVehiclesAsync(
            string? make,
            string? model,
            string? plateStart,
            string? plateEnd,
            int? id);

        Task<object> AddVehicleAsync(VehicleDTO input);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
