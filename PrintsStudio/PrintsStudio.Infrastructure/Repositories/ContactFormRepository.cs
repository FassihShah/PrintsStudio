using Microsoft.EntityFrameworkCore;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Domain.Interfaces;
using PrintsStudio.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintsStudio.Infrastructure.Repositories
{
    public class ContactFormRepository : IContactFormRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactForm>> GetAllAsync()
        {
            return await _context.ContactForms.ToListAsync();
        }

        public async Task<ContactForm?> GetByIdAsync(int id)
        {
            return await _context.ContactForms
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(ContactForm booking)
        {
            await _context.ContactForms.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ContactForm booking)
        {
            _context.ContactForms.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.ContactForms.FindAsync(id);
            if (booking != null)
            {
                _context.ContactForms.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
