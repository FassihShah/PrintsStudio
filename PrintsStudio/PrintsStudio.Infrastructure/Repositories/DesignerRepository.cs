using Microsoft.EntityFrameworkCore;
using PrintsStudio.Domain.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Infrastructure.Identity;

namespace PrintsStudio.Server.Repositories
{
    public class DesignerRepository : IDesignerRepository
    {
        private readonly ApplicationDbContext _context;

        public DesignerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Designer>> GetAllAsync()
        {
            return await _context.Designers.ToListAsync();
        }

        public async Task<Designer?> GetByIdAsync(int id)
        {
            return await _context.Designers
                .FirstOrDefaultAsync(d => d.DesignerId == id);
        }

        public async Task AddAsync(Designer designer)
        {
            await _context.Designers.AddAsync(designer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Designer designer)
        {
            _context.Designers.Update(designer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var designer = await _context.Designers.FindAsync(id);
            if (designer != null)
            {
                _context.Designers.Remove(designer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
