using Library.BusinessLayer.Services.Interfaces;
using Library.DataLayer.Models;
using Library.DataLayer.Models.Dto;
using Library.DataLayer.Repository.Interfaces;

namespace Library.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }

        public async Task<UserModel> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.RegisterUserAsync(userDto, cancellationToken);

            return user;
        }
    }
}
