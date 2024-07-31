using CargoPay.Application.Services;
using CargoPay.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CargoPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserRepository _userRepository;

        public AuthController(AuthService authService, UserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetUserByUsernameAndPasswordAsync(request.Username, request.Password);
            if (user != null)
            {
                var token = _authService.GenerateJwtToken(request.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
