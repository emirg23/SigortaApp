using Microsoft.EntityFrameworkCore;
using SigortaApp.Models;
using SigortaApp.Repositories.Interfaces;

namespace SigortaApp.Repositories
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly MyDbContext _context;

        public InsuranceRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Insurance>> GetAllAsync(int? id, string? companyName)
        {
            var query = _context.Insurances.AsQueryable();

            if (id.HasValue)
                query = query.Where(i => i.Id == id);

            if (!string.IsNullOrEmpty(companyName))
                query = query.Where(i => i.CompanyName == companyName);

            return await query.ToListAsync();
        }

        public async Task<List<Insurance>> GetExpiredAsync()
        {
            var today = DateTime.UtcNow;
            return await _context.Insurances
                .Where(i => i.ExpirationAt < today)
                .ToListAsync();
        }

        public async Task<Insurance?> GetByIdAsync(int id)
        {
            return await _context.Insurances.FindAsync(id);
        }

        public async Task AddAsync(Insurance insurance)
        {
            await _context.Insurances.AddAsync(insurance);
        }

        public async Task DeleteAsync(Insurance insurance)
        {
            _context.Insurances.Remove(insurance);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
