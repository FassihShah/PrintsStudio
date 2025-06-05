using scd_project.Models;
using System.Net.Http.Json;

namespace scd_project.Services
{
    public class OrderItemService
    {
        private readonly HttpClient _http;

        public OrderItemService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<OrderItem>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<OrderItem>>("api/OrderItem");
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<OrderItem>($"api/OrderItem/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(OrderItem orderItem)
        {
            return await _http.PostAsJsonAsync("api/OrderItem", orderItem);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, OrderItem orderItem)
        {
            return await _http.PutAsJsonAsync($"api/OrderItem/{id}", orderItem);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/OrderItem/{id}");
        }
    }
}
