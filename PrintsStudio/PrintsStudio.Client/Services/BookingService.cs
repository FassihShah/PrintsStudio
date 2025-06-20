using PrintsStudio.Client.Models;
using System.Net.Http.Json;

namespace PrintsStudio.Client.Services
{
    public class BookingService
    {
        private readonly HttpClient _http;

        public BookingService(HttpClient http)
        {
            _http = http;
        }

        // Get all bookings
        public async Task<List<Booking>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<Booking>>("api/Booking");
        }

        // Get a single booking by ID
        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Booking>($"api/Booking/{id}");
        }

        // Create a new booking
        public async Task<HttpResponseMessage> CreateAsync(Booking booking)
        {
            return await _http.PostAsJsonAsync("api/Booking", booking);
        }

        // Update an existing booking
        public async Task<HttpResponseMessage> UpdateAsync(int id, Booking booking)
        {
            return await _http.PutAsJsonAsync($"api/Booking/{id}", booking);
        }

        // Delete a booking
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"api/Booking/{id}");
        }
    }
}