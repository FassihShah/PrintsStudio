using PrintsStudio.Client.Models;
using System.Net.Http.Json;

namespace PrintsStudio.Client.Services
{
    public class OrderService
    {
        private readonly HttpClient _http;

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Order>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<Order>>("api/Order");
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Order>($"api/Order/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(Order order)
        {
            return await _http.PostAsJsonAsync("api/Order", order);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, Order order)
        {
            return await _http.PutAsJsonAsync($"api/Order/{id}", order);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/Order/{id}");
        }
    }
}
