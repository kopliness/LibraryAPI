using Library.Business.Dto;
using Library.DAL.Models;

namespace Library.Business.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string?> GetUserTokenAsync(UserDto userDto, CancellationToken cancellationToken = default);
    }
}