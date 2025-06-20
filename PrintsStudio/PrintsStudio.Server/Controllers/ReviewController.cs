using Microsoft.AspNetCore.Mvc;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAll()
        {
            var reviews = await _service.GetAllAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetById(int id)
        {
            var review = await _service.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            await _service.AddAsync(review);
            return CreatedAtAction(nameof(GetById), new { id = review.ReviewId }, review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Review review)
        {
            if (id != review.ReviewId)
                return BadRequest();

            await _service.UpdateAsync(review);
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
