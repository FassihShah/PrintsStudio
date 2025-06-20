using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Domain.Interfaces;

namespace PrintsStudio.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Review review)
        {
            await _repository.AddAsync(review);
        }

        public async Task UpdateAsync(Review review)
        {
            await _repository.UpdateAsync(review);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
