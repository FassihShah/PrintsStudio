using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintsStudio.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Booking booking)
        {
            await _repository.AddAsync(booking);
        }

        public async Task UpdateAsync(Booking booking)
        {
            await _repository.UpdateAsync(booking);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}