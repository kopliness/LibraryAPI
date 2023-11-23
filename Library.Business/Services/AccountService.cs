using System.Security.Cryptography;
using AutoMapper;
using Library.Business.Dto;
using Library.Business.Services.Interfaces;
using Library.Common.Exceptions;
using Library.DAL.Entities;
using Library.DAL.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Business.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<AccountService> _logger;
    private readonly IMapper _mapper;


    public AccountService(IAccountRepository accountRepository, IMapper mapper, ILogger<AccountService> logger)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<AccountDto> RegisterAccountAsync(AccountDto accountDto,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting account registration");

        var accountModel = _mapper.Map<AccountDto, Account>(accountDto);

        var loginExists = await _accountRepository.IsLoginTakenAsync(accountModel.Login, cancellationToken);
        if (loginExists)
        {
            _logger.LogError("Login is already taken");
            throw new ExistsException("Login is already taken.");
        }

        var salt = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var pbkdf2 = new Rfc2898DeriveBytes(accountModel.Password, salt, 100000, HashAlgorithmName.SHA256);
        var hashedPassword = pbkdf2.GetBytes(32);

        accountModel.Password = Convert.ToBase64String(hashedPassword);
        accountModel.Salt = Convert.ToBase64String(salt);

        var account = await _accountRepository.RegisterAccountAsync(accountModel, cancellationToken);

        _logger.LogInformation("Account registered successfully");

        return _mapper.Map<Account, AccountDto>(account);
    }
}