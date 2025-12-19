using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigortaApp.DTOs;
using SigortaApp.Services.Interfaces;

namespace SigortaApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(
            [FromQuery] string? firstName,
            [FromQuery] string? lastName,
            [FromQuery] int? id,
            [FromQuery] string? email,
            [FromQuery] string? role)
        {
            var result = await _userService.GetUsersAsync(
                firstName, lastName, id, email, role);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] RegisterUserDTO input)
        {
            await _userService.RegisterAsync(input);
            return Ok(new { message = "User created successfully." });
        }

        [HttpPatch("{id}/role")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] string newRole)
        {
            try
            {
                var success = await _userService.UpdateUserRoleAsync(id, newRole);
                if (!success)
                    return NotFound(new { message = "User not found." });

                return Ok(new { message = "User role updated." });
            }
            catch
            {
                return BadRequest(new
                {
                    message = "Invalid role. Must be 'admin', 'logistics', or 'accounting'."
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound(new { message = "User not found." });

            return Ok(new { message = "User deleted successfully." });
        }
    }
}