namespace PrintsStudio.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using PrintsStudio.Domain.Interfaces;
    using PrintsStudio.Domain.Entities;
    using PrintsStudio.Infrastructure.Identity;

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.CustomizationOptions)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.CustomizationOptions)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product updatedProduct)
        {
            var existingProduct = await _context.Products
                .Include(p => p.CustomizationOptions)
                .FirstOrDefaultAsync(p => p.ProductId == updatedProduct.ProductId);

            if (existingProduct != null)
            {
                // Update scalar properties
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.BasePrice = updatedProduct.BasePrice;

                // Handle Images (ensure you're storing and updating them properly)
                existingProduct.Images = updatedProduct.Images;

                // Optional: handle nested collections manually to avoid replacing all data
                // e.g., Remove deleted templates, update existing, add new

                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
