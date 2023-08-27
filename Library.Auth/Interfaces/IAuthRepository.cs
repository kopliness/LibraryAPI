using Library.Domain.Dto;

namespace Library.Auth.Interfaces
{
    public interface IAuthRepository
    {
        Task<string?> GetUserTokenAsync(UserDto userDto, CancellationToken cancellationToken = default );
    }
}
