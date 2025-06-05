using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Domain.Interfaces
{
    public interface IDesignerRepository
    {
        Task<IEnumerable<Designer>> GetAllAsync();
        Task<Designer?> GetByIdAsync(int id);
        Task AddAsync(Designer designer);
        Task UpdateAsync(Designer designer);
        Task DeleteAsync(int id);
    }
}
