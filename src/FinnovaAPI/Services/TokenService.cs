using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinnovaAPI.Models;
using FinnovaAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FinnovaAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string GenerateToken(BankClientModel bankClient)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims:
                [
                    new Claim(type: ClaimTypes.NameIdentifier, bankClient.Id.ToString()),
                    new Claim(type: ClaimTypes.Name, bankClient.Name)
                ],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
    }
}