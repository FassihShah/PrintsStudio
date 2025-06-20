using PrintsStudio.Client.Models;
using System.Net.Http.Json;

namespace PrintsStudio.Client.Services
{
    public class DesignerService
    {
        private readonly HttpClient _http;

        public DesignerService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Designer>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<Designer>>("api/Designer");
        }

        public async Task<Designer?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Designer>($"api/Designer/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(Designer designer)
        {
            return await _http.PostAsJsonAsync("api/Designer", designer);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, Designer designer)
        {
            return await _http.PutAsJsonAsync($"api/Designer/{id}", designer);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/Designer/{id}");
        }
    }
}
