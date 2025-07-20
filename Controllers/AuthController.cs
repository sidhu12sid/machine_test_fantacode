using login_app.DTOs;
using login_app.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace login_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserCeateDto userCreate)
        {
            try
            {
                var result = await _authService.CreateUser(userCreate);
                if (result != null)
                {
                    return StatusCode(200, new
                    {
                        error = false,
                        status = true,
                        message = "User created successfully.",
                        data = new { }
                    });
                }
                return StatusCode(400, new
                {
                    error = true,
                    status = false,
                    message = "Failed to create the user",
                    data = new { }
                });
            }
            catch (Exception ex) 
            {
                return StatusCode(417, new
                {
                    error = true,
                    status = false,
                    message = ex.Message,
                    data = new { }
                });
            }

        }


        [EnableRateLimiting("fixed")]
        [HttpPost("user-login")]      
        public async Task<IActionResult> AuthenticateUser([FromBody] UserLogin userLogin)
        {
            try
            {
                var result = await _authService.UserLogin(userLogin);
                if (!result.status)
                {
                    return StatusCode(401, result);
                }
                else
                {
                    return StatusCode(200, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(417, new
                {
                    error = true,
                    status = false,
                    message = ex.Message,
                    data = new { }
                });
            }
        }
    }
}
