using AutoMapper;
using Library.Business.Dto;
using Library.Business.Services.Interfaces;
using Library.DAL.Models;
using Library.DAL.Repository.Interfaces;

namespace Library.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            var userModel = _mapper.Map<UserDto, User>(userDto);
            
            var user = await _userRepository.RegisterUserAsync(userModel, cancellationToken);
            
            return user;
        }
    }
}