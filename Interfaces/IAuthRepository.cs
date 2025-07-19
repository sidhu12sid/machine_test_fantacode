using login_app.DTOs;
using login_app.Models;

namespace login_app.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> CreateUser(User user);
        Task<User?> GetUserByName(string? userName);
    }
}
