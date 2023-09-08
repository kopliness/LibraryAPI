using System.Security.Claims;

namespace Library.BusinessLayer.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(List<Claim> claims);
    }
}