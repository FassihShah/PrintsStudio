using PrintsStudio.Client.Models;
using System.Net.Http.Json;

namespace PrintsStudio.Client.Services
{
    public class UserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UserDTO?> GetCurrentUserAsync()
        {
            return await _http.GetFromJsonAsync<UserDTO>("api/User/current");
        }

        public async Task<UserDTO?> GetByIdAsync(string id)
        {
            return await _http.GetFromJsonAsync<UserDTO>($"api/User/{id}");
        }

        public async Task<List<UserDTO>?> GetAllUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserDTO>>("api/User");
        }

        public async Task<List<UserDTO>?> GetDesignersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserDTO>>("api/User/designers");
        }

        public async Task<List<UserDTO>?> GetAdminsAsync()
        {
            return await _http.GetFromJsonAsync<List<UserDTO>>("api/User/admins");
        }

        public async Task<HttpResponseMessage> UpdateUserAsync(UserDTO userDto)
        {
            return await _http.PutAsJsonAsync("api/User", userDto);
        }

        public async Task<HttpResponseMessage> DeleteUserAsync(string id)
        {
            return await _http.DeleteAsync($"api/User/{id}");
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginModel lm)
        {
           
            return await _http.PostAsJsonAsync($"api/User/login", lm);
        }

        public async Task<HttpResponseMessage> LogoutAsync()
        {
            return await _http.PostAsync("api/User/logout", null);
        }

        public async Task<HttpResponseMessage> RegisterAsync(RegisterModel rm)
        {

            return await _http.PostAsJsonAsync("api/User/register", rm);
        }

        public async Task<HttpResponseMessage> RegisterDesignerAsync(
            string fullName,
            string email,
            string password,
            string bio,
            string portfolioUrl,
            string profileImageUrl,
            bool isAvailable)
        {
            var query = $"?fullName={fullName}&email={email}&password={password}&bio={bio}&portfolioUrl={portfolioUrl}&profileImageUrl={profileImageUrl}&isAvailable={isAvailable}";
            return await _http.PostAsync($"api/User/register-designer{query}", null);
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            return await _http.GetFromJsonAsync<UserDTO>($"api/User/email/{email}");
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            return await _http.GetFromJsonAsync<bool>("api/User/isauthenticated");
        }

        public async Task<bool> IsSignedInAsync()
        {
            return await _http.GetFromJsonAsync<bool>("api/User/issignedin");
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string role)
        {
            return await _http.GetFromJsonAsync<bool>($"api/User/isinrole/{userId}/{role}");
        }

        public async Task<HttpResponseMessage> SeedAsync()
        {
            return await _http.PostAsync("api/User/seed", null);
        }
    }
}
