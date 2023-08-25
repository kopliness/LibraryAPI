using Library.Domain.Dto;
using Library.Domain.Models;

namespace Library.Database.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto?> GetUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
        Task<UserModel> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    }
}
