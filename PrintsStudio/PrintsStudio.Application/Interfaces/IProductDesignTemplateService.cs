using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Application.Interfaces
{
    public interface IProductDesignTemplateService
    {
        Task<IEnumerable<ProductDesignTemplate>> GetAllAsync();
        Task<ProductDesignTemplate?> GetByIdAsync(int id);
        Task AddAsync(ProductDesignTemplate template);
        Task UpdateAsync(ProductDesignTemplate template);
        Task DeleteAsync(int id);
    }
}
