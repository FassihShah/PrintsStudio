using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PrintsStudio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactFormController : ControllerBase
    {
        private readonly IContactFormService _service;

        public ContactFormController(IContactFormService service)
        {
            _service = service;
        }

        // Admin-only: Get all contact messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactForm>>> GetAll()
        {
            var contacts = await _service.GetAllAsync();
            return Ok(contacts);
        }

        // Optional: Get a specific message by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactForm>> GetById(int id)
        {
            var contact = await _service.GetByIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // POST: Submit a contact form
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] ContactForm contact)
        {
            await _service.AddAsync(contact);
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
        }

        // Optional: Delete message
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
