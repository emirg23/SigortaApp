using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sigorta.DTOs;
using SigortaApp.Services.Interfaces;

namespace SigortaApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _service;

        public VehiclesController(IVehicleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetVehicles(
            [FromQuery] string? make,
            [FromQuery] string? model,
            [FromQuery] string? plateStart,
            [FromQuery] string? plateEnd,
            [FromQuery] int? id)
        {
            var result = await _service.GetVehiclesAsync(make, model, plateStart, plateEnd, id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDTO input)
        {
            var vehicle = await _service.AddVehicleAsync(input);
            return Created("", vehicle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var deleted = await _service.DeleteVehicleAsync(id);
            if (!deleted)
                return NotFound(new { message = "Vehicle not found." });

            return Ok(new { message = $"Vehicle {id} has been deleted." });
        }
    }
}
