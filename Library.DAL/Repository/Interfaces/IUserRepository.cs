using Library.DAL.Models;

namespace Library.DAL.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(User user, CancellationToken cancellationToken = default);
        Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken = default);
    }
}