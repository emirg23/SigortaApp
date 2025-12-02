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
    public class InsurancesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public InsurancesController(MyDbContext context)
        {
            _context = context;
        }

        // GET /insurances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetInsurances()
        {
            var insurances = await _context.Insurances
                .Select(i => new
                {
                    i.Id,
                    i.ExpirationAt,
                    i.CompanyName,
                    i.CreatedAt
                })
                .ToListAsync();

            return Ok(insurances);
        }

        // GET /insurances/expired
        [HttpGet("expired")]
        public async Task<ActionResult<IEnumerable<object>>> GetExpiredInsurances()
        {
            var today = DateTime.UtcNow;

            var expiredInsurances = await _context.Insurances
                .Where(i => i.ExpirationAt < today)
                .Select(i => new
                {
                    i.Id,
                    i.ExpirationAt,
                    i.CompanyName,
                    i.CreatedAt
                })
                .ToListAsync();

            return Ok(expiredInsurances);
        }

        // POST /insurances
        [HttpPost]
        public async Task<ActionResult> CreateInsurance([FromBody] InsuranceInput input)
        {
            var insurance = new Insurance
            {
                Id = input.Id,
                ExpirationAt = input.ExpirationAt,
                CompanyName = input.CompanyName,
                CreatedAt = DateTime.UtcNow
            };

            _context.Insurances.Add(insurance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInsurances), new { id = insurance.Id }, insurance);
        }

        // DELETE /insurances/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurance(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance == null)
            {
                return NotFound(new { message = "Insurance not found." });
            }

            _context.Insurances.Remove(insurance);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Insurance {insurance.Id} has been deleted." });
        }

    }
}
