using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PrintsStudio.Application;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Infrastructure.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetCurrentUserIdAsync()
    {
        return _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
    }

    public async Task<UserDTO> GetByIdAsync(string userId)
    {
        var user = await _userManager.Users
            .Include(u => u.DesignerProfile)
            .Include(u => u.Orders)
            .Include(u => u.Reviews)
            .FirstOrDefaultAsync(u => u.Id == userId);

        return MapToDto(user);
    }

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userManager.Users
            .Include(u => u.DesignerProfile)
            .Include(u => u.Orders)
            .Include(u => u.Reviews)
            .ToListAsync();

        return users.Select(MapToDto).ToList();
    }

    public async Task<List<UserDTO>> GetDesignersAsync()
    {
        var users = await _userManager.GetUsersInRoleAsync("Designer");
        return users.Select(MapToDto).ToList();
    }

    public async Task<List<UserDTO>> GetAdminsAsync()
    {
        var users = await _userManager.GetUsersInRoleAsync("Admin");
        return users.Select(MapToDto).ToList();
    }

    public async Task<bool> UpdateUserAsync(UserDTO userDto)
    {
        var user = await _userManager.Users
            .Include(u => u.DesignerProfile)
            .FirstOrDefaultAsync(u => u.Id == userDto.Id);

        if (user == null)
            return false;

        user.FullName = userDto.FullName;
        user.Email = userDto.Email;
        user.PhoneNumber = userDto.PhoneNumber;
        user.ProfileImageUrl = userDto.ProfileImageUrl;

        if (!string.IsNullOrEmpty(userDto.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, userDto.Password);
        }

        if (userDto.Role == "Designer" && userDto.DesignerProfile != null)
        {
            if (user.DesignerProfile == null)
                user.DesignerProfile = new Designer();

            user.DesignerProfile.Bio = userDto.DesignerProfile.Bio;
            user.DesignerProfile.PortfolioUrl = userDto.DesignerProfile.PortfolioUrl;
            user.DesignerProfile.ProfileImageUrl = userDto.DesignerProfile.ProfileImageUrl;
            user.DesignerProfile.IsAvailable = userDto.DesignerProfile.IsAvailable;
        }

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
    }

    public async Task<bool> LoginUserAsync(string email, string password, bool rememberMe)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return false;

        var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> CreateUserAsync(RegisterModel rm)
    {
        var user = new ApplicationUser
        {
            FullName = rm.FullName,
            Email = rm.Email,
            UserName = rm.Email,
            Role = rm.Role,
            PhoneNumber=rm.PhoneNumber,
            ProfileImageUrl= rm.ProfileImageUrl,
            
        };

        var result = await _userManager.CreateAsync(user, rm.Password);
        if (!result.Succeeded) return false;

        await _userManager.AddToRoleAsync(user, rm.Role);
        return true;
    }

    public async Task<bool> CreateDesignerAsync(string fullName, string email, string password, string bio, string portfolioUrl, string profileImageUrl, bool isAvailable)
    {
        var user = new ApplicationUser
        {
            FullName = fullName,
            Email = email,
            UserName = email,
            Role = "Designer",
            DesignerProfile = new Designer
            {
                Bio = bio,
                PortfolioUrl = portfolioUrl,
                ProfileImageUrl = profileImageUrl,
                IsAvailable = isAvailable
            }
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) return false;

        await _userManager.AddToRoleAsync(user, "Designer");
        return true;
    }

    public async Task<UserDTO> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.Users
            .Include(u => u.DesignerProfile)
            .Include(u => u.Orders)
            .Include(u => u.Reviews)
            .FirstOrDefaultAsync(u => u.Email == email);

        return MapToDto(user);
    }

    public async Task<bool> IsUserInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> IsSignedIn()
    {
        return _signInManager.IsSignedIn(_httpContextAccessor.HttpContext.User);
    }

    public async Task<bool> IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public async Task SeedRolesAndUsers()
    {
        string[] roles = { "Admin", "Designer", "Customer" };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = "admin@printsstudio.com";
        if (await _userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                PhoneNumber="234444",
                FullName = "Admin",
                Role = "Admin",
                ProfileImageUrl="/hjjj"
                
            };

            var result = await _userManager.CreateAsync(admin, "Admin@123");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }

    private UserDTO MapToDto(ApplicationUser user)
    {
        if (user == null) return null;

        return new UserDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            ProfileImageUrl = user.ProfileImageUrl,
            Password = "", // Never return password for security
            DesignerProfile = user.DesignerProfile,
            Orders = user.Orders,
            Reviews = user.Reviews
        };
    }
}
