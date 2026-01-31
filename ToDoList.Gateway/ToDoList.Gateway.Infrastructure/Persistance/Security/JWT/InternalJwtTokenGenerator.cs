using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ToDoList.Gateway.Infrastructure.Persistance.Security.JWT
{
    public class InternalJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        
        public InternalJwtTokenGenerator(IConfiguration config)
        {
            _configuration = config;
        }

        public (string Token, DateTime ExpiredAt) Generate()
        {
            var expiresAt = DateTime.UtcNow.AddMinutes(5);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["InternalJwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["InternalJwt:Issuer"],
                audience: _configuration["InternalJwt:Audience"],
                expires: expiresAt,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }
    }
}
