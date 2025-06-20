using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Domain.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task<OrderItem?> GetByIdAsync(int id);
        Task AddAsync(OrderItem oi);
        Task UpdateAsync(OrderItem oi);
        Task DeleteAsync(int id);
    }
}
