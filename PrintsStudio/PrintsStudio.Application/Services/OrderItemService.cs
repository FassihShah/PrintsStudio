using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Domain.Interfaces;

namespace PrintsStudio.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _repository;

        public OrderItemService(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
           return await _repository.GetAllAsync();
        }
        public async Task<OrderItem?> GetByIdAsync(int id)
        {
           return await _repository.GetByIdAsync(id);
        }
        public async Task AddAsync(OrderItem oi)
        {
           await _repository.AddAsync(oi);
        }
        public async Task UpdateAsync(OrderItem oi)
        {
           await _repository.UpdateAsync(oi);
        }
        public async Task DeleteAsync(int id)
        {
           await _repository.DeleteAsync(id);
        }
    }
}
