using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task AddAsync(Review r);
        Task UpdateAsync(Review r);
        Task DeleteAsync(int id);
    }
}
