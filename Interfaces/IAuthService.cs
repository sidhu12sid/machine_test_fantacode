using login_app.DTOs;
using login_app.Models;

namespace login_app.Interfaces
{
    public interface IAuthService
    {
        Task<User?> CreateUser(UserCeateDto createDto);
    }
}
