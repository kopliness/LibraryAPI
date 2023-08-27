using Library.Auth.Interfaces;
using Library.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Auth
{
    public class GenerateToken :IGenerateToken
    {
        private readonly JwtOptionsModel _jwtOptions;

        public GenerateToken(IOptions<JwtOptionsModel> jwtOptions) => _jwtOptions = jwtOptions.Value;

        public Task<string> GenerateTokenAsync(List<Claim> claims)
        {
            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var jwt = new JwtSecurityToken(_jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                expires: DateTime.Now.Add(TimeSpan.FromHours(1)),
                notBefore: DateTime.Now,
                signingCredentials: new(singingKey, SecurityAlgorithms.HmacSha256));

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwt));
        }
    }
}
