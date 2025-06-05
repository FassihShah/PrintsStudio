using Microsoft.EntityFrameworkCore;
using PrintsStudio.Domain.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Infrastructure.Identity;

namespace PrintsStudio.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .Include(oi => oi.SelectedDesignTemplate)
                .Include(oi => oi.AppliedCustomizations)
                .ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .Include(oi => oi.SelectedDesignTemplate)
                .Include(oi => oi.AppliedCustomizations)
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task AddAsync(OrderItem oi)
        {
            await _context.OrderItems.AddAsync(oi);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem oi)
        {
            _context.OrderItems.Update(oi);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item != null)
            {
                _context.OrderItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
