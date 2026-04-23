using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintsStudio.Application;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;
using PrintsStudio.Infrastructure.Identity;

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

    public Task<string> GetCurrentUserIdAsync()
    {
        return Task.FromResult(_userManager.GetUserId(_httpContextAccessor.HttpContext?.User));
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
        {
            return false;
        }

        user.FullName = userDto.FullName;
        user.Email = userDto.Email;
        user.UserName = userDto.Email;
        user.PhoneNumber = userDto.PhoneNumber;
        user.ProfileImageUrl = userDto.ProfileImageUrl;
        user.Role = userDto.Role;

        if (!string.IsNullOrWhiteSpace(userDto.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, userDto.Password);
        }

        if (userDto.DesignerProfile != null)
        {
            user.DesignerProfile = new Designer
            {
                UserId = user.Id,
                Bio = userDto.DesignerProfile.Bio,
                PortfolioUrl = userDto.DesignerProfile.PortfolioUrl,
                ProfileImageUrl = userDto.DesignerProfile.ProfileImageUrl,
                IsAvailable = userDto.DesignerProfile.IsAvailable
            };
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

    public async Task<AuthResult> LoginUserAsync(string email, string password, bool rememberMe)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new AuthResult
            {
                Succeeded = false,
                Message = "Account not found."
            };
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
        if (result.Succeeded)
        {
            return new AuthResult
            {
                Succeeded = true,
                Message = "Login successful."
            };
        }

        return new AuthResult
        {
            Succeeded = false,
            Message = "Invalid email or password."
        };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<AuthResult> CreateUserAsync(RegisterModel rm)
    {
        var allowedRoles = new[] { "Customer", "Designer" };
        if (!allowedRoles.Contains(rm.Role))
        {
            return new AuthResult
            {
                Succeeded = false,
                Message = "Invalid role selected.",
                Errors = new List<string> { "Public signup cannot create admin users." }
            };
        }

        var existingUser = await _userManager.FindByEmailAsync(rm.Email);
        if (existingUser != null)
        {
            return new AuthResult
            {
                Succeeded = false,
                Message = "Email is already registered.",
                Errors = new List<string> { "An account with this email already exists." }
            };
        }

        var user = new ApplicationUser
        {
            FullName = rm.FullName,
            Email = rm.Email,
            UserName = rm.Email,
            Role = rm.Role,
            PhoneNumber = rm.PhoneNumber,
            ProfileImageUrl = rm.ProfileImageUrl
        };

        var result = await _userManager.CreateAsync(user, rm.Password);
        if (!result.Succeeded)
        {
            return new AuthResult
            {
                Succeeded = false,
                Message = "Registration failed.",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        await _userManager.AddToRoleAsync(user, rm.Role);
        return new AuthResult
        {
            Succeeded = true,
            Message = "User registered successfully."
        };
    }

    public async Task<AuthResult> CreateDesignerAsync(string fullName, string email, string password, string bio, string portfolioUrl, string profileImageUrl, bool isAvailable)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            return new AuthResult
            {
                Succeeded = false,
                Message = "Email is already registered.",
                Errors = new List<string> { "An account with this email already exists." }
            };
        }

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
        if (!result.Succeeded)
        {
            return new AuthResult
            {
                Succeeded = false,
                Message = "Designer registration failed.",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        await _userManager.AddToRoleAsync(user, "Designer");
        return new AuthResult
        {
            Succeeded = true,
            Message = "Designer registered successfully."
        };
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
        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public Task<bool> IsSignedIn()
    {
        var isSignedIn = _signInManager.IsSignedIn(_httpContextAccessor.HttpContext?.User);
        return Task.FromResult(isSignedIn);
    }

    public Task<bool> IsAuthenticated()
    {
        var isAuthenticated = _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        return Task.FromResult(isAuthenticated);
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
                PhoneNumber = "234444",
                FullName = "Admin",
                Role = "Admin",
                ProfileImageUrl = "/hjjj"
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
        if (user == null)
        {
            return null;
        }

        return new UserDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            ProfileImageUrl = user.ProfileImageUrl,
            Password = "",
            DesignerProfile = user.DesignerProfile,
            Orders = user.Orders,
            Reviews = user.Reviews
        };
    }
}
