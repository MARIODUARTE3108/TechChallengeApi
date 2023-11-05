using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Services
{
    public class TokenApplicationService
    {
        private readonly IConfiguration _configuration;

        public TokenApplicationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.Email),
               
            };

            var chave = Encoding.ASCII.GetBytes(_configuration["AppSettings:SecurityKey"]);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(chave),
            SecurityAlgorithms.HmacSha256Signature);


            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddDays(30),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
