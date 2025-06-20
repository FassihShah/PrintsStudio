using Microsoft.AspNetCore.Mvc;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _service;

        public OrderItemController(IOrderItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItem orderItem)
        {
            await _service.AddAsync(orderItem);
            return CreatedAtAction(nameof(GetById), new { id = orderItem.Id }, orderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
                return BadRequest();

            await _service.UpdateAsync(orderItem);
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
