using login_app.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace login_app.Services
{
    public class JWTHandler : IJWTHandler
    {
        private readonly IConfiguration _configuration;

        public JWTHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string? GenerateJwtToken(string? userId, string? userName)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                                {
                                    new Claim(JwtRegisteredClaimNames.Sub, userId), 
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64), 
                                    new Claim(ClaimTypes.Name, userName) 
                                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30), 
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
