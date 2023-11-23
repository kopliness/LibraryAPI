using Library.DAL.Entities;

namespace Library.DAL.Repository.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetAccountAsync(Account account, CancellationToken cancellationToken = default);
    Task<Account> RegisterAccountAsync(Account account, CancellationToken cancellationToken = default);
    Task<bool> IsLoginTakenAsync(string login, CancellationToken cancellationToken = default);
}