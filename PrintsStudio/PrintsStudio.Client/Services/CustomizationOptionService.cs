using PrintsStudio.Client.Models;
using System.Net.Http.Json;

namespace PrintsStudio.Client.Services
{
    public class CustomizationOptionService
    {
        private readonly HttpClient _http;

        public CustomizationOptionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CustomizationOption>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<CustomizationOption>>("api/CustomizationOption");
        }

        public async Task<CustomizationOption?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<CustomizationOption>($"api/CustomizationOption/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(CustomizationOption option)
        {
            return await _http.PostAsJsonAsync("api/CustomizationOption", option);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, CustomizationOption option)
        {
            return await _http.PutAsJsonAsync($"api/CustomizationOption/{id}", option);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/CustomizationOption/{id}");
        }
    }
}
