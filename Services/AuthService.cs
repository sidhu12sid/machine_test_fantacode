using login_app.DTOs;
using login_app.Interfaces;
using login_app.Models;

namespace login_app.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHashingService _passwordHasher;
        private readonly IJWTHandler _jwtHandler;
        public AuthService(IAuthRepository authRepository, IPasswordHashingService passwordHasher, IJWTHandler jWTHandler)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jWTHandler;
        }

        public async Task<User?> CreateUser(UserCeateDto createDto)
        {
            try
            {
                User user = new User();
                user.Username = createDto.Username;
                user.Password = _passwordHasher.HashPassword(createDto.Password);
                user.Email = createDto.Email;

                var result = await _authRepository.CreateUser(user);
                if (result != null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseModel<LoginResponseModel>> UserLogin(UserLogin login)
        {
            ResponseModel<LoginResponseModel> userLoginDeatils = new ResponseModel<LoginResponseModel>();
            try
            {
                var result = await _authRepository.GetUserByName(login.UserName ?? "");
                if(result != null)
                {
                    var isPasswordOk = _passwordHasher.VerifyPassword(login?.Password, result?.Password); //validating the user password.
                    LoginResponseModel? response = new LoginResponseModel();
                    if (isPasswordOk)
                    {
                        var accessToken = _jwtHandler.GenerateJwtToken(result?.Id.ToString(), result?.Username);
                        response.AccessToken = accessToken;
                        response.UserName = result?.Username;


                    }
                    else
                    {
                        response = null;
                    }

                    
                    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
            return userLoginDeatils;
        }
    }
}


