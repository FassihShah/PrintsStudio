using Microsoft.AspNetCore.Authorization;
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
            if (userId == null)
            {
                Console.WriteLine("hey" + userId);
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
                return NotFound();

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
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel lm)
        {
            var success = await _userService.LoginUserAsync(lm.Email, lm.Password, lm.RememberMe);
            if (!success)
                return Unauthorized("Invalid credentials");

            return Ok("Login successful");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return Ok("Logged out");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel rm)
        {
            var success = await _userService.CreateUserAsync(rm);
            if (!success)
                return BadRequest("User registration failed");

            return Ok("User registered successfully");
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
            var success = await _userService.CreateDesignerAsync(
                fullName, email, password, bio, portfolioUrl, profileImageUrl, isAvailable
            );

            if (!success)
                return BadRequest("Designer registration failed");

            return Ok("Designer registered successfully");
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDTO>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound();

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
            return Ok("Seeding complete");
        }
        [HttpPost("upload-profile-image")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploadsFolder); // ensure the folder exists

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string fileUrl = $"/uploads/{uniqueFileName}"; // Relative URL for DB/frontend
            return Ok(new { Url = fileUrl });
        }

    }
}
