using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilitiesController(AvailabilityService service) : ControllerBase
    {
        private readonly AvailabilityService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilities()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Availability>> GetAvailability(int id)
        {
            var availability = await _service.GetByIdAsync(id);
            if (availability == null) return NotFound();
            return Ok(availability);
        }

        [HttpPost]
        public async Task<ActionResult<Availability>> PostAvailability(Availability availability)
        {
            await _service.AddAsync(availability);
            return CreatedAtAction(nameof(GetAvailability), new { id = availability.Id }, availability);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvailability(int id, Availability availability)
        {
            if (id != availability.Id) return BadRequest();
            await _service.UpdateAsync(availability);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
