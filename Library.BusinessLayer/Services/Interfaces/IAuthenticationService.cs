using Library.BusinessLayer.Dto;
using Library.DataLayer.Models;

namespace Library.BusinessLayer.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string?> GetUserTokenAsync(UserDto userDto, CancellationToken cancellationToken = default);
    }
}