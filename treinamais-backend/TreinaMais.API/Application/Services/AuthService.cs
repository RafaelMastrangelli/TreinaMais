using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EducaDev.API.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EducaDev.API.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<AuthResult?> AuthenticateAsync(string email, string password)
        {
            // Simple demo user validation; replace with real user store
            var user = GetValidUser(email, password);
            if (user == null)
                return Task.FromResult<AuthResult?>(null);

            var jwtSection = _configuration.GetSection("Jwt");
            var key = jwtSection["Key"]!;
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expiresMinutes = int.TryParse(jwtSection["ExpiresMinutes"], out var m) ? m : 60;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds);

            var encoded = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult<AuthResult?>(new AuthResult
            {
                Token = encoded,
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email
            });
        }

        private static User? GetValidUser(string email, string password)
        {
            // Very simple static user for demo purposes
            if (email == "admin@treinamais.com" && password == "admin")
            {
                return new User
                {
                    Id = 1,
                    Name = "Administrador",
                    Email = "admin@treinamais.com"
                };
            }
            return null;
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }
    }
}


