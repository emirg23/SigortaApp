using Microsoft.EntityFrameworkCore;
using SigortaApp.DTOs;
using SigortaApp.Models;
using SigortaApp.Repositories.Interfaces;
using SigortaApp.Services.Interfaces;

namespace SigortaApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<object>> GetUsersAsync(
            string? firstName,
            string? lastName,
            int? id,
            string? email,
            string? role)
        {
            var query = _userRepository.GetAll();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(u => u.FirstName.ToUpper().StartsWith(firstName.ToUpper()));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(u => u.LastName.ToUpper().StartsWith(lastName.ToUpper()));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.Email == email);

            if (!string.IsNullOrEmpty(role))
                query = query.Where(u => u.Role == role);

            if (id.HasValue)
                query = query.Where(u => u.Id == id);

            return await query
                .Select(u => new
                {
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Role,
                    u.Email,
                    u.CreatedAt
                })
                .ToListAsync();
        }

        public async Task AddUserAsync(UserDTO input)
        {
            var user = new User
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                Role = "user",
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserRoleAsync(int id, string newRole)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            var allowedRoles = new[] { "admin", "logistics", "accounting" };
            if (!allowedRoles.Contains(newRole.ToLower()))
                throw new Exception("Invalid role");

            user.Role = newRole.ToLower();
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}