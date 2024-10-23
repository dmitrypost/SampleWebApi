using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SampleWebApi.Infrastructure.Configuration;

namespace SampleWebApi.Infrastructure.Helpers
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }

    public class TokenGenerator(string jwtSecret, int daysExpiresIn) : ITokenGenerator
    {
        public string GenerateToken(User user)
        {
            
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(daysExpiresIn),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecret)
                    ),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
