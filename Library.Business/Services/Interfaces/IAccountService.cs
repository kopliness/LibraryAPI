using Library.Business.Dto;

namespace Library.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> RegisterAccountAsync(AccountDto accountDto, CancellationToken cancellationToken = default);
    }
}