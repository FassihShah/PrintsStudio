using scd_project.Models;
using System.Net.Http.Json;

namespace scd_project.Services
{
    public class ProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Product>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<Product>>("api/Product");
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Product>($"api/Product/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(Product product)
        {
            return await _http.PostAsJsonAsync("api/Product", product);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, Product product)
        {
            return await _http.PutAsJsonAsync($"api/Product/{id}", product);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/Product/{id}");
        }
    }
}
