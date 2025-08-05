using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;

namespace TodoApi.Controllers 
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result)
                return BadRequest("Username already exists");

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            var response = await _authService.LoginAsync(dto);
            if (response == null)
                return Unauthorized("Invalid username or password");

            return Ok(new { Message = "Login successful", response });
        }
    }
}
