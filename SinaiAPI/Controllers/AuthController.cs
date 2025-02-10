using Microsoft.AspNetCore.Mvc;
using SinaiAPI.DTOs;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            try
            {
                var user = await _authService.Register(request);
                return Ok(user);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var token = await _authService.Login(request);

            if (token == null)
            {
                return Unauthorized(new { error = "Invalid username or password" });
            }

            return Ok(new { token });
        }
    }
}
