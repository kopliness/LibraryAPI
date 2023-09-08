using Library.DataLayer.Models.Dto;
using Library.DataLayer.Models;

namespace Library.DataLayer.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto?> GetUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
        Task<UserModel> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    }
}