using MessagingSystemApp.Application.Abstractions.Services.TokenServices;
using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Domain.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace MessagingSystemApp.Infrastructure.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token GenerateAccessToken(Employee user, int day)
        {
            Token token=new Token();
            token.Expiration=DateTime.Now.AddDays(2);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials:signingCredentials
            );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

            token.RefreshToken =GenerateRefreshToken();
           
            return token;
        }

        public string GenerateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator randomNumber= RandomNumberGenerator.Create();
            randomNumber.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
