using PrintsStudio.Client.Models;
using System.Text.Json;
using Blazored.LocalStorage; // If using Blazored.LocalStorage NuGet


namespace PrintsStudio.Client.Services
{
    public class UserCartService
    {
        private const string CartStorageKey = "user_cart";
        private readonly ILocalStorageService _localStorage;

        public UserCartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<CartViewModel> GetCartAsync()
        {
            var cart = await _localStorage.GetItemAsync<CartViewModel>(CartStorageKey);
            return cart ?? new CartViewModel();
        }

        public async Task AddToCartAsync(OrderItem item)
        {
            var cart = await GetCartAsync();
            cart.OrderItems.Add(item);
            await _localStorage.SetItemAsync(CartStorageKey, cart);
        }

        public async Task ClearCartAsync()
        {
            await _localStorage.RemoveItemAsync(CartStorageKey);
        }
    }

}
