using Microsoft.AspNetCore.Mvc;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomizationOptionController : ControllerBase
    {
        private readonly ICustomizationOptionService _service;

        public CustomizationOptionController(ICustomizationOptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomizationOption>>> GetAll()
        {
            var options = await _service.GetAllAsync();
            return Ok(options);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomizationOption>> GetById(int id)
        {
            var option = await _service.GetByIdAsync(id);
            if (option == null)
                return NotFound();

            return Ok(option);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomizationOption option)
        {
            await _service.AddAsync(option);
            return CreatedAtAction(nameof(GetById), new { id = option.CustomizationOptionId }, option);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomizationOption option)
        {
            if (id != option.CustomizationOptionId)
                return BadRequest();

            await _service.UpdateAsync(option);
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
