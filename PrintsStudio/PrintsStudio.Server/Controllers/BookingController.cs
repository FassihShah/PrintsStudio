using Microsoft.AspNetCore.Mvc;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintsStudio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
        {
            var bookings = await _service.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetById(int id)
        {
            var booking = await _service.GetByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            await _service.AddAsync(booking);
            return CreatedAtAction(nameof(GetById), new { id = booking.id }, booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Booking booking)
        {
            if (id != booking.id)
                return BadRequest();

            await _service.UpdateAsync(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}