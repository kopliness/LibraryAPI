using Library.DataLayer.Models;

namespace Library.DataLayer.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUserAsync(UserModel userModel, CancellationToken cancellationToken = default);
        Task<UserModel> RegisterUserAsync(UserModel userModel, CancellationToken cancellationToken = default);
    }
}