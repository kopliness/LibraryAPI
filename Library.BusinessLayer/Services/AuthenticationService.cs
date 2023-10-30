using Library.DataLayer.Repository.Interfaces;
using System.Security.Claims;
using AutoMapper;
using Library.BusinessLayer.Dto;
using Library.BusinessLayer.Services.Interfaces;
using Library.DataLayer.Models;
using Library.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace Library.BusinessLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper,
            ILogger<AuthenticationService> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string?> GetUserTokenAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting user token");
            cancellationToken.ThrowIfCancellationRequested();

            var userModel = _mapper.Map<UserDto, UserModel>(userDto);

            var user = await _userRepository.GetUserAsync(userModel, cancellationToken);

            if (user == null)
            {
                _logger.LogError($"User with this login:{userModel.Login} is not found.");
                throw new NotFoundException($"User with this login:{userModel.Login} is not found.");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Login)
            };

            var token = await _tokenService.GenerateTokenAsync(claims);

            if (token == null)
            {
                _logger.LogError("Token not found.");
                throw new NotFoundException("Token not found.");
            }
            _logger.LogInformation("Receipt of the user token was successful.");

            return token;
        }
    }
}