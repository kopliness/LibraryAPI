using System.Security.Authentication;
using Library.DAL.Repository.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using Library.Business.Dto;
using Library.Business.Services.Interfaces;
using Library.Common.Exceptions;
using Library.DAL.Entities;
using Microsoft.Extensions.Logging;

namespace Library.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;

        private readonly IAccountRepository _accountRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IAccountRepository accountRepository, ITokenService tokenService, IMapper mapper,
            ILogger<AuthenticationService> logger)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string?> GetAccountTokenAsync(AccountDto accountDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting account token");
            cancellationToken.ThrowIfCancellationRequested();

            var accountModel = _mapper.Map<AccountDto, Account>(accountDto);

            var account = await _accountRepository.GetAccountAsync(accountModel, cancellationToken);

            if (account == null)
            {
                _logger.LogError("Account with this login is not found.");
                throw new NotFoundException("Account with this login is not found.");
            }
            
            var pbkdf2 = new Rfc2898DeriveBytes(accountDto.Password, Convert.FromBase64String(account.Salt), 100000, HashAlgorithmName.SHA256);
            var hashedPassword = pbkdf2.GetBytes(32);

            if (Convert.ToBase64String(hashedPassword) != account.Password)
            {
                _logger.LogError("Incorrect password.");
                throw new AuthenticationException("Incorrect password.");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, account.Login)
            };

            var token = _tokenService.GenerateTokenAsync(claims);

            if (token == null)
            {
                _logger.LogError("Token not found.");
                throw new NotFoundException("Token not found.");
            }
            _logger.LogInformation("Receipt of the account token was successful.");

            return token;
        }


    }
}