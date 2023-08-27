using System.Security.Claims;

namespace Library.Auth.Interfaces
{
    public interface IGenerateToken
    {
        Task<string> GenerateTokenAsync (List<Claim> claims);
    }
}
