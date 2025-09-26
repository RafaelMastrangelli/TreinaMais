using EducaDev.API.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EducaDev.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public record LoginRequest(string Email, string Password);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.AuthenticateAsync(request.Email, request.Password);
            if (result is null) return Unauthorized(new { message = "Credenciais inválidas" });
            
            return Ok(new { 
                token = result.Token,
                user = new {
                    id = result.UserId,
                    name = result.UserName,
                    email = result.Email
                }
            });
        }
    }
}


