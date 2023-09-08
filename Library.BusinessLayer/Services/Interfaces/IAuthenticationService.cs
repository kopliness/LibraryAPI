using Library.DataLayer.Models.Dto;

namespace Library.BusinessLayer.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string?> GetUserTokenAsync(UserDto userDto, CancellationToken cancellationToken = default);
    }
}