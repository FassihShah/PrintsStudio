using PrintsStudio.Client.Models;
using System.Net.Http.Json;

namespace PrintsStudio.Client.Services
{
    public class ContactFormService
    {
        private readonly HttpClient _http;

        public ContactFormService(HttpClient http)
        {
            _http = http;
        }

        // Get all contact messages (admin use)
        public async Task<List<ContactForm>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ContactForm>>("api/ContactForm");
        }

        // Get a specific contact message by ID
        public async Task<ContactForm?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ContactForm>($"api/ContactForm/{id}");
        }

        // Submit a new contact form
        public async Task<HttpResponseMessage> SubmitAsync(ContactForm contact)
        {
            return await _http.PostAsJsonAsync("api/ContactForm", contact);
        }

        // Delete a contact form message (optional, admin only)
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/ContactForm/{id}");
        }
    }
}
