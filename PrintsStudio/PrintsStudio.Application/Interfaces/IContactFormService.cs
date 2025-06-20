using PrintsStudio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintsStudio.Application.Interfaces
{
    public interface IContactFormService
    {
        Task<IEnumerable<ContactForm>> GetAllAsync();
        Task<ContactForm?> GetByIdAsync(int id);
        Task AddAsync(ContactForm booking);
        Task UpdateAsync(ContactForm booking);
        Task DeleteAsync(int id);
    }
}
