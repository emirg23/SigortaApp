using SigortaApp.DTOs;
using SigortaApp.Models;
using SigortaApp.Repositories.Interfaces;
using SigortaApp.Services.Interfaces;

namespace SigortaApp.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IInsuranceRepository _repository;

        public InsuranceService(IInsuranceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InsuranceDTO>> GetAllAsync(int? id, string? companyName)
        {
            var insurances = await _repository.GetAllAsync(id, companyName);

            return insurances.Select(i => new InsuranceDTO
            {
                Id = i.Id,
                CompanyName = i.CompanyName,
                ExpirationAt = i.ExpirationAt
            }).ToList();
        }


        public async Task<List<InsuranceDTO>> GetExpiredAsync()
        {
            var insurances = await _repository.GetExpiredAsync();

            return insurances.Select(i => new InsuranceDTO
            {
                Id = i.Id,
                CompanyName = i.CompanyName,
                ExpirationAt = i.ExpirationAt,
            }).ToList();
        }

        public async Task<InsuranceDTO> CreateAsync(InsuranceDTO input)
        {
            var insurance = new Insurance
            {
                CompanyName = input.CompanyName,
                ExpirationAt = input.ExpirationAt,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(insurance);
            await _repository.SaveChangesAsync();

            input.Id = insurance.Id;

            return input;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var insurance = await _repository.GetByIdAsync(id);
            if (insurance == null)
                return false;

            await _repository.DeleteAsync(insurance);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
