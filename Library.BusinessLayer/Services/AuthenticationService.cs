using Library.DataLayer.Repository.Interfaces;
using System.Security.Claims;
using Library.DataLayer.Models.Dto;
using Library.BusinessLayer.Services.Interfaces;
using Library.Shared.Exceptions;

namespace Library.BusinessLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;

        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<string?> GetUserTokenAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetUserAsync(userDto, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.Login)
            };

            var token = await _tokenService.GenerateTokenAsync(claims);

            if (token == null)
            {
                throw new NotFoundException("Token not found.");
            }

            return token;
        }
    }
}
