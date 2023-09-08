using Library.DataLayer.Models.Dto;
using Library.DataLayer.Context;
using Library.DataLayer.Repository.Interfaces;
using Library.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.DataLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _context;

        private readonly ILogger<UserRepository> _logger;

        public UserRepository(LibraryContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserDto?> GetUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _context.Users.FirstOrDefaultAsync(
                user => user.Login.Equals(userDto.Login) && user.Password.Equals(userDto.Password), cancellationToken);

            return user == null ? null : new UserDto(user.Login, user.Password);
        }

        public async Task<UserModel> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _context.Users.AddAsync(new UserModel()
            {
                Login = userDto.Login,
                Password = userDto.Password
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Зарегистрирован новый пользователь {user.Entity.Login}");

            return user.Entity;
        }
    }
}