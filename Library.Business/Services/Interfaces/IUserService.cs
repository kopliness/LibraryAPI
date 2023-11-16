using Library.Business.Dto;
using Library.DAL.Models;

namespace Library.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    }
}