using AutoMapper;
using Library.BusinessLayer.Dto;
using Library.BusinessLayer.Services.Interfaces;
using Library.DataLayer.Models;
using Library.DataLayer.Repository.Interfaces;

namespace Library.BusinessLayer.Services
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

        public async Task<UserModel> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            var userModel = _mapper.Map<UserDto, UserModel>(userDto);
            
            var user = await _userRepository.RegisterUserAsync(userModel, cancellationToken);
            
            return user;
        }
    }
}