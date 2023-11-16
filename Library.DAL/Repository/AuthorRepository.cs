using Library.DAL.Context;
using Library.DAL.Models;
using Library.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repository;

public class AuthorRepository: IAuthorRepository
{
    private readonly LibraryContext _context;
    
    public AuthorRepository(LibraryContext context)=> _context = context;
    
    public async Task<Author?> CreateAsync(Author author, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var authorEntity = await _context.AddAsync(author, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return authorEntity.Entity;
    }

    public List<Author> ReadAll()
    {
        return _context.Authors
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Book)
            .AsNoTracking()
            .ToList();
    }
    public async Task<Author?> UpdateAsync(Guid id, Author author, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var authors = await _context.Authors
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Book)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (author == null)
        {
            return null;
        }

        authors.FirstName = author.FirstName;
        authors.LastName = author.LastName;

        authors.BookAuthors = author.BookAuthors.Select(ba => new BookAuthor
        {
            AuthorId = authors.Id,
            BookId = ba.BookId
        }).ToList();

        await _context.SaveChangesAsync(cancellationToken);

        return authors;
    }

    public async Task<Author?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
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
    public async Task<Author?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }
}