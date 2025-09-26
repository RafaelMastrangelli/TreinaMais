namespace EducaDev.API.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult?> AuthenticateAsync(string email, string password);
    }

    public class AuthResult
    {
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}


