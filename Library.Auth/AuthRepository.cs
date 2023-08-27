using Library.Auth.Interfaces;
using Library.Database.Repository.Interfaces;
using Library.Domain.Dto;
using System.Security.Claims;

namespace Library.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IGenerateToken _generateToken;

        private readonly IUserRepository _userRepository;

        public AuthRepository(IUserRepository userRepository, IGenerateToken generateToken)
        {
            _userRepository = userRepository;
            _generateToken = generateToken;
        }
        public async Task<string?> GetUserTokenAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetUserAsync(userDto, cancellationToken);

            if(user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.Login)
            };

            var token = await _generateToken.GenerateTokenAsync(claims);

            return token;
        }
    }
}
