using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintsStudio.Application.Services
{
    public class ContactFormService : IContactFormService
    {
        private readonly IContactFormRepository _repository;

        public ContactFormService(IContactFormRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ContactForm>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ContactForm?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(ContactForm booking)
        {
            await _repository.AddAsync(booking);
        }

        public async Task UpdateAsync(ContactForm booking)
        {
            await _repository.UpdateAsync(booking);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
