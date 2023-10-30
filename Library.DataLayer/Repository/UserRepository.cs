using Library.DataLayer.Context;
using Library.DataLayer.Repository.Interfaces;
using Library.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.DataLayer.Repository;

public class UserRepository : IUserRepository
{
    private readonly LibraryContext _context;
    
    public UserRepository(LibraryContext context)=> _context = context;

    public async Task<UserModel?> GetUserAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _context.Users.FirstOrDefaultAsync(
            user => user.Login.ToUpper().Equals(userModel.Login.ToUpper()) && 
                    user.Password.Equals(userModel.Password), cancellationToken);

        return user;
    }



    public async Task<UserModel> RegisterUserAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _context.Users.AddAsync(userModel);

        await _context.SaveChangesAsync(cancellationToken);
        
        return user.Entity;
    }
}