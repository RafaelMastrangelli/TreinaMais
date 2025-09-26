using EducaDev.API.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EducaDev.API.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação de usuários
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Modelo de requisição para login
        /// </summary>
        /// <param name="Email">Email do usuário</param>
        /// <param name="Password">Senha do usuário</param>
        public record LoginRequest(string Email, string Password);

        /// <summary>
        /// Realiza login do usuário
        /// </summary>
        /// <param name="request">Dados de login (email e senha)</param>
        /// <returns>Token JWT e informações do usuário</returns>
        /// <response code="200">Login realizado com sucesso</response>
        /// <response code="401">Credenciais inválidas</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
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


