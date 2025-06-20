using PrintsStudio.Client.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace PrintsStudio.Client.Services
{
    public class ProductDesignTemplateService
    {
        private readonly HttpClient _http;
        public ProductDesignTemplateService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProductDesignTemplate>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ProductDesignTemplate>>("api/ProductDesignTemplate");
        }

        public async Task<ProductDesignTemplate?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ProductDesignTemplate>($"api/ProductDesignTemplate/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(ProductDesignTemplate template)
        {
            return await _http.PostAsJsonAsync("api/ProductDesignTemplate", template);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, ProductDesignTemplate template)
        {
            return await _http.PutAsJsonAsync($"api/ProductDesignTemplate/{id}", template);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/ProductDesignTemplate/{id}");
        }
    }
}
