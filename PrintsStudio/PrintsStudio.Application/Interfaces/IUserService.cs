using Microsoft.AspNetCore.Mvc;
using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> GetCurrentUserIdAsync();
        Task<UserDTO> GetByIdAsync(string userId);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<List<UserDTO>> GetDesignersAsync();
        Task<List<UserDTO>> GetAdminsAsync();
        Task<bool> UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(string userId);
        Task<bool> LoginUserAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
        Task<bool> CreateUserAsync(RegisterModel rm);
        Task<bool> CreateDesignerAsync(string fullName, string email, string password, string bio, string portfolioUrl, string profileImageUrl, bool isAvailable);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<bool> IsUserInRoleAsync(string userId, string role);
        Task<bool> IsSignedIn();
        Task<bool> IsAuthenticated();
        Task SeedRolesAndUsers();
    }
}
