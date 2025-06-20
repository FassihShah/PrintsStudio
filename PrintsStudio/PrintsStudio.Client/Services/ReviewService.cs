using PrintsStudio.Client.Models;
using System.Net.Http.Json;

namespace PrintsStudio.Client.Services
{
    public class ReviewService
    {
        private readonly HttpClient _http;

        public ReviewService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Review>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<Review>>("api/Review");
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Review>($"api/Review/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(Review review)
        {
            return await _http.PostAsJsonAsync("api/Review", review);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, Review review)
        {
            return await _http.PutAsJsonAsync($"api/Review/{id}", review);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/Review/{id}");
        }
    }
}
