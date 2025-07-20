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

        public async Task<ResponseModel<LoginResponse>> UserLogin(UserLogin login)
        {
            var response = new ResponseModel<LoginResponse>();
            try
            {
                var result = await _authRepository.GetUserByName(login.UserName ?? "");
                if (result != null)
                {
                    //Validating the password
                    var isPasswordOk = _passwordHasher.VerifyPassword(login.Password, result.Password);
                    if (!isPasswordOk)
                    {
                        response.status = false;
                        response.error = true;
                        response.message = "Incorrect password";
                        response.data = new LoginResponse { };
                    }
                    else
                    {
                        var accesssToken = _jwtHandler.GenerateJwtToken(result.Id.ToString(), result.Username);
                        LoginResponse userDetails = new LoginResponse()
                        {
                            AccessToken = accesssToken,
                            UserName = result.Username,
                        };

                        response.status = true;
                        response.error = false;
                        response.message = "Login successfull";
                        response.data = userDetails;
                    }
                }
                else
                {
                    response.status = false;
                    response.error = true;
                    response.message = "Incorrect username";
                    response.data = new LoginResponse { };
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
            return response;
        }
    }
}


