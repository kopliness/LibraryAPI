using Library.DataLayer.Context;
using Library.DataLayer.Models;
using Library.DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DataLayer.Repository;

public class AuthorRepository: IAuthorRepository
{
    private readonly LibraryContext _context;
    
    public AuthorRepository(LibraryContext context)=> _context = context;
    
    public async Task<AuthorModel?> CreateAsync(AuthorModel authorModel, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var authorEntity = await _context.AddAsync(authorModel, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return authorEntity.Entity;
    }

    public List<AuthorModel> ReadAll()
    {
        return _context.Authors
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Book)
            .AsNoTracking()
            .ToList();
    }
    public async Task<AuthorModel?> UpdateAsync(Guid id, AuthorModel authorModel, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var author = await _context.Authors
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Book)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (author == null)
        {
            return null;
        }

        author.FirstName = authorModel.FirstName;
        author.LastName = authorModel.LastName;

        author.BookAuthors = authorModel.BookAuthors.Select(ba => new BookAuthor
        {
            AuthorId = author.Id,
            BookId = ba.BookId
        }).ToList();

        await _context.SaveChangesAsync(cancellationToken);

        return author;
    }

    public async Task<AuthorModel?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var author = await _context.Authors.FirstOrDefaultAsync(author=>author.Id==id);

        if (author == null)
        {
            return null;
        }
        
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync(cancellationToken);
        
        return author;
    }
    public async Task<AuthorModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }
}