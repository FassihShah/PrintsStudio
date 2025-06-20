using Microsoft.AspNetCore.Mvc;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDesignTemplateController : ControllerBase
    {
        private readonly IProductDesignTemplateService _service;

        public ProductDesignTemplateController(IProductDesignTemplateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDesignTemplate>>> GetAll()
        {
            var templates = await _service.GetAllAsync();
            return Ok(templates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDesignTemplate>> GetById(int id)
        {
            var template = await _service.GetByIdAsync(id);
            if (template == null)
                return NotFound();

            return Ok(template);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDesignTemplate template)
        {
            await _service.AddAsync(template);
            return CreatedAtAction(nameof(GetById), new { id = template.id }, template);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDesignTemplate template)
        {
            if (id != template.id)
                return BadRequest();

            await _service.UpdateAsync(template);
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
