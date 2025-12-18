using SigortaApp.DTOs;

namespace SigortaApp.Services.Interfaces
{
    public interface IInsuranceService
    {
        Task<List<InsuranceDTO>> GetAllAsync(int? id, string? companyName);
        Task<List<InsuranceDTO>> GetExpiredAsync();
        Task<InsuranceDTO> CreateAsync(InsuranceDTO input);
        Task<bool> DeleteAsync(int id);
    }
}
