using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Domain.Interfaces;

namespace PrintsStudio.Application.Services
{
    public class CustomizationOptionService : ICustomizationOptionService
    {
        private readonly ICustomizationOptionRepository _repository;

        public CustomizationOptionService(ICustomizationOptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomizationOption>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CustomizationOption?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(CustomizationOption option)
        {
            await _repository.AddAsync(option);
        }

        public async Task UpdateAsync(CustomizationOption option)
        {
            await _repository.UpdateAsync(option);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
