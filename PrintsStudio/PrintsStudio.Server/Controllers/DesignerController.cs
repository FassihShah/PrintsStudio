using Microsoft.AspNetCore.Mvc;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignerController : ControllerBase
    {
        private readonly IDesignerService _service;

        public DesignerController(IDesignerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Designer>>> GetAll()
        {
            var designers = await _service.GetAllAsync();
            return Ok(designers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Designer>> GetById(int id)
        {
            var designer = await _service.GetByIdAsync(id);
            if (designer == null)
                return NotFound();

            return Ok(designer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Designer designer)
        {
            await _service.AddAsync(designer);
            return CreatedAtAction(nameof(GetById), new { id = designer.DesignerId }, designer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Designer designer)
        {
            if (id != designer.DesignerId)
                return BadRequest();

            await _service.UpdateAsync(designer);
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
