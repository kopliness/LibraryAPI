using Library.DAL.Context;
using Library.DAL.Repository.Interfaces;
using Library.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.DAL.Repository;

public class UserRepository : IUserRepository
{
    private readonly LibraryContext _context;
    
    public UserRepository(LibraryContext context)=> _context = context;

    public async Task<User?> GetUserAsync(User user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var users = await _context.Users.FirstOrDefaultAsync(
            user => user.Login.ToUpper().Equals(user.Login.ToUpper()) && 
                    user.Password.Equals(user.Password), cancellationToken);

        return users;
    }



    public async Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var users = await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync(cancellationToken);
        
        return users.Entity;
    }
}