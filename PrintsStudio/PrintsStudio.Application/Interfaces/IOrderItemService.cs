using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task<OrderItem?> GetByIdAsync(int id);
        Task AddAsync(OrderItem oi);
        Task UpdateAsync(OrderItem oi);
        Task DeleteAsync(int id);
    }
}
