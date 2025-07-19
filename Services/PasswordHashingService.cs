using login_app.Interfaces;

namespace login_app.Services
{
    public class PasswordHashingService : IPasswordHashingService
    {
        public string HashPassword(string? password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        public bool VerifyPassword(string? password, string? passwordHash)      
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
