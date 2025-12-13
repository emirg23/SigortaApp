using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigortaApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SigortaApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public VehiclesController(MyDbContext context)
        {
            _context = context;
        }

        // GET /vehicles?make=Toyota&model=Corolla
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetVehicles(
            [FromQuery] string? make,
            [FromQuery] string? model,
            [FromQuery] string? plateStart,
            [FromQuery] string? plateEnd,
            [FromQuery] int? id
            )
        {
            var query = _context.Vehicles.AsQueryable();
            
            if (!string.IsNullOrEmpty(make))
                query = query.Where(v => v.Make.ToLower() == make.ToLower());

            if (!string.IsNullOrEmpty(model))
                query = query.Where(v => v.Model.ToLower() == model.ToLower());

            if (id.HasValue)
                query = query.Where(v => v.Id == id);

            var vehicles = await query
                .Select(v => new {
                    v.Id,
                    v.Plate,
                    v.Make,
                    v.Model,
                    v.CreatedAt,
                    Letters = ExtractLetters(v.Plate)
                })
                .ToListAsync();

            if (!string.IsNullOrEmpty(plateStart))
                vehicles = vehicles.Where(v => v.Letters.StartsWith(plateStart.ToUpper())).ToList();

            if (!string.IsNullOrEmpty(plateEnd))
                vehicles = vehicles.Where(v => v.Letters.EndsWith(plateEnd.ToUpper())).ToList();

            var result = vehicles
                .OrderBy(v => v.Letters)
                .Select(v => new {
                    v.Id,
                    v.Plate,
                    v.Make,
                    v.Model,
                    v.CreatedAt
                });

            return Ok(result);
        }


        private static string ExtractLetters(string plate)
        {
            return new string(plate.Where(char.IsLetter).ToArray()).ToUpper();
        }


        // POST /vehicles
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleInput input)
        {
            var vehicle = new Vehicle
            {
                Id = input.Id,
                Plate = input.Plate,
                Make = input.Make,
                Model = input.Model,
                CreatedAt = DateTime.UtcNow
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicles), new { id = vehicle.Id }, vehicle);
        }

        // DELETE /vehicles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound(new { message = "Vehicle not found." });
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Vehicle {vehicle.Id} has been deleted." });
        }
    }
}
