using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Domain.Interfaces
{
    public interface ICustomizationOptionRepository
    {
        Task<IEnumerable<CustomizationOption>> GetAllAsync();
        Task<CustomizationOption?> GetByIdAsync(int id);
        Task AddAsync(CustomizationOption option);
        Task UpdateAsync(CustomizationOption option);
        Task DeleteAsync(int id);
    }
}
