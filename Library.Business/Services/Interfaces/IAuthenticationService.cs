using Library.Business.Dto;

namespace Library.Business.Services.Interfaces;

public interface IAuthenticationService
{
    Task<string?> GetAccountTokenAsync(AccountDto accountDto, CancellationToken cancellationToken = default);
}