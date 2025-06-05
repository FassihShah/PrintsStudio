using Microsoft.EntityFrameworkCore;
using PrintsStudio.Domain.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Infrastructure.Identity;

namespace PrintsStudio.Infrastructure.Repositories
{
    public class ReviewRepository:IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context= context; 
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.Product)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
        }

        public async Task AddAsync(Review r)
        {
            await _context.Reviews.AddAsync(r);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review r)
        {
            _context.Reviews.Update(r);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
