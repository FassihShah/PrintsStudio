using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Domain.Interfaces
{
    public interface IProductDesignTemplateRepository
    {
        Task<IEnumerable<ProductDesignTemplate>> GetAllAsync();
        Task<ProductDesignTemplate?> GetByIdAsync(int id);
        Task AddAsync(ProductDesignTemplate template);
        Task UpdateAsync(ProductDesignTemplate template);
        Task DeleteAsync(int id);


    }
}
