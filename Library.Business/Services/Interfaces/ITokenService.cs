using System.Security.Claims;

namespace Library.Business.Services.Interfaces;

public interface ITokenService
{
    string GenerateTokenAsync(List<Claim> claims);
}