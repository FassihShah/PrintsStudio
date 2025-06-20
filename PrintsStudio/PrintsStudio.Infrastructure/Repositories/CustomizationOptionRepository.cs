using Microsoft.EntityFrameworkCore;
using PrintsStudio.Domain.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Infrastructure.Identity;

namespace PrintsStudio.Infrastructure.Repositories
{
    public class CustomizationOptionRepository: ICustomizationOptionRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomizationOptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomizationOption>> GetAllAsync()
        {
            return await _context.CustomizationOptions
                .ToListAsync();
        }

        public async Task<CustomizationOption?> GetByIdAsync(int id)
        {
            return await _context.CustomizationOptions
                .FirstOrDefaultAsync(c => c.CustomizationOptionId == id);
        }

        public async Task AddAsync(CustomizationOption option)
        {
            await _context.CustomizationOptions.AddAsync(option);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomizationOption option)
        {
            _context.CustomizationOptions.Update(option);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var option = await _context.CustomizationOptions.FindAsync(id);
            if (option != null)
            {
                _context.CustomizationOptions.Remove(option);
                await _context.SaveChangesAsync();
            }
        }
    }
}
