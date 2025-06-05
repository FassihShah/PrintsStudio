using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Domain.Interfaces;

namespace PrintsStudio.Application.Services
{
    public class DesignerService : IDesignerService
    {
        private readonly IDesignerRepository _repository;

        public DesignerService(IDesignerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Designer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Designer?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddAsync(Designer designer)
        {
           await _repository.AddAsync(designer);
        }
        public async Task UpdateAsync(Designer designer)
        {
          await _repository.UpdateAsync(designer);
        }
        public async Task DeleteAsync(int id)
        {
           await _repository.DeleteAsync(id);
        }
    }
}
