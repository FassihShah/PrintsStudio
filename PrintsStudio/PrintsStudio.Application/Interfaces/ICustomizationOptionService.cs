using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Application.Interfaces
{
    public interface ICustomizationOptionService
    {
        Task<IEnumerable<CustomizationOption>> GetAllAsync();
        Task<CustomizationOption?> GetByIdAsync(int id);
        Task AddAsync(CustomizationOption option);
        Task UpdateAsync(CustomizationOption option);
        Task DeleteAsync(int id);
    }
}
