using Microsoft.EntityFrameworkCore;
using PrintsStudio.Domain.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Infrastructure.Identity;

namespace PrintsStudio.Infrastructure.Repositories
{
    public class ProductDesignTemplateRepository:IProductDesignTemplateRepository

    {
        private readonly ApplicationDbContext _context;

        public ProductDesignTemplateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDesignTemplate>> GetAllAsync()
        {
            return await _context.ProductDesignTemplates
                .Include(t => t.Product)
                .ToListAsync();
        }

        public async Task<ProductDesignTemplate?> GetByIdAsync(int id)
        {
            return await _context.ProductDesignTemplates
                .Include(t => t.Product)
                .FirstOrDefaultAsync(t => t.ProductDesignTemplateId == id);
        }

        public async Task AddAsync(ProductDesignTemplate template)
        {
            await _context.ProductDesignTemplates.AddAsync(template);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductDesignTemplate template)
        {
            _context.ProductDesignTemplates.Update(template);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var template = await _context.ProductDesignTemplates.FindAsync(id);
            if (template != null)
            {
                _context.ProductDesignTemplates.Remove(template);
                await _context.SaveChangesAsync();
            }
        }
    }
}
