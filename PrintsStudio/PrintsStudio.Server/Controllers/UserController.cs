using Microsoft.AspNetCore.Mvc;
using PrintsStudio.Application;
using PrintsStudio.Application.Interfaces;
using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("current")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var userId = await _userService.GetCurrentUserIdAsync();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var user = await _userService.GetByIdAsync(userId);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("designers")]
        public async Task<ActionResult<List<UserDTO>>> GetDesigners()
        {
            return Ok(await _userService.GetDesignersAsync());
        }

        [HttpGet("admins")]
        public async Task<ActionResult<List<UserDTO>>> GetAdmins()
        {
            return Ok(await _userService.GetAdminsAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDTO userDto)
        {
            var result = await _userService.UpdateUserAsync(userDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel lm)
        {
            var result = await _userService.LoginUserAsync(lm.Email, lm.Password, lm.RememberMe);
            if (!result.Succeeded)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return Ok(new AuthResult { Succeeded = true, Message = "Logged out." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel rm)
        {
            var result = await _userService.CreateUserAsync(rm);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register-designer")]
        public async Task<IActionResult> RegisterDesigner(
            string fullName,
            string email,
            string password,
            string bio,
            string portfolioUrl,
            string profileImageUrl,
            bool isAvailable)
        {
            var result = await _userService.CreateDesignerAsync(
                fullName, email, password, bio, portfolioUrl, profileImageUrl, isAvailable
            );

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDTO>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("isauthenticated")]
        public async Task<ActionResult<bool>> IsAuthenticated()
        {
            return Ok(await _userService.IsAuthenticated());
        }

        [HttpGet("issignedin")]
        public async Task<ActionResult<bool>> IsSignedIn()
        {
            return Ok(await _userService.IsSignedIn());
        }

        [HttpGet("isinrole/{userId}/{role}")]
        public async Task<ActionResult<bool>> IsUserInRole(string userId, string role)
        {
            return Ok(await _userService.IsUserInRoleAsync(userId, role));
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            await _userService.SeedRolesAndUsers();
            return Ok(new AuthResult { Succeeded = true, Message = "Seeding complete." });
        }

        [HttpPost("upload-profile-image")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { Url = $"/uploads/{uniqueFileName}" });
        }
    }
}
