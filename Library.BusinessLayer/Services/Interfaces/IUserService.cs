using Library.DataLayer.Models.Dto;
using Library.DataLayer.Models;

namespace Library.BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    }
}