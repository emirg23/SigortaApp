using SigortaApp.DTOs;
using SigortaApp.Models;

namespace SigortaApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<object>> GetUsersAsync(
            string? firstName,
            string? lastName,
            int? id,
            string? email,
            string? role);

        Task AddUserAsync(UserDTO input);
        Task<bool> UpdateUserRoleAsync(int id, string newRole);
        Task<bool> DeleteUserAsync(int id);
    }
}