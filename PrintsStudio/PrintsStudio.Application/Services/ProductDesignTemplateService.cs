using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Domain.Interfaces;

namespace PrintsStudio.Application.Services
{
    public class ProductDesignTemplateService : IProductDesignTemplateService
    {
        private readonly IProductDesignTemplateRepository _repository;

        public ProductDesignTemplateService(IProductDesignTemplateRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDesignTemplate>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductDesignTemplate?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(ProductDesignTemplate template)
        {
            await _repository.AddAsync(template);
        }

        public async Task UpdateAsync(ProductDesignTemplate template)
        {
            await _repository.UpdateAsync(template);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
