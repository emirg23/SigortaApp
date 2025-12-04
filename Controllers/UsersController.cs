using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigortaApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigortaApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

            // GET /users
            [HttpGet]
            public async Task<ActionResult<IEnumerable<object>>> GetUsers()
            {
                var users = await _context.Users
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

                return Ok(users);
            }

            // POST /users
            [HttpPost]
            public async Task<IActionResult> AddUser([FromBody] UserInput input)
            {
                var user = new User
                {
                    Id = input.Id,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Role = "user",
                    Email = input.Email,
                    CreatedAt = DateTime.UtcNow 
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
            }

            // PATCH /users/{id}/role
            [HttpPatch("{id}/role")]
            public async Task<IActionResult> UpdateUserRole(int id, [FromBody] string newRole)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                var allowedRoles = new[] { "admin", "logistics", "accounting" };
                if (!allowedRoles.Contains(newRole.ToLower()))
                {
                    return BadRequest(new { message = "Invalid role. Must be 'admin', 'logistics', or 'accounting'." });
                }

                user.Role = newRole.ToLower();
                await _context.SaveChangesAsync();

                return Ok(new { message = $"User {user.FirstName} role updated to '{user.Role}'." });
            }
            
            // DELETE /users/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteUser(int id)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"User {user.FirstName} has been deleted." });
            }

    }
}
