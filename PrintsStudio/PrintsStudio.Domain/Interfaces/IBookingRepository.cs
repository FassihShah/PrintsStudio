using PrintsStudio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintsStudio.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
    }
}