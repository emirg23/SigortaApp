using Microsoft.AspNetCore.Mvc;
using SigortaApp.DTOs;
using SigortaApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SigortaApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InsurancesController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsurancesController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetInsurances(
            [FromQuery] int? id,
            [FromQuery] string? companyName)
        {
            var result = await _insuranceService.GetAllAsync(id, companyName);
            return Ok(result);
        }

        [HttpGet("expired")]
        public async Task<IActionResult> GetExpiredInsurances()
        {
            var result = await _insuranceService.GetExpiredAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInsurance([FromBody] InsuranceDTO input)
        {
            var created = await _insuranceService.CreateAsync(input);
            return CreatedAtAction(nameof(GetInsurances), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurance(int id)
        {
            var success = await _insuranceService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
