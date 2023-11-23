using Library.DAL.Context;
using Library.DAL.Entities;
using Library.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryContext _context;

    public AuthorRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<Author?> CreateAsync(Author newAuthor, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var author = await _context.AddAsync(newAuthor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return author.Entity;
    }

    public async Task<List<Author>> ReadAllAsync()
    {
        return await _context.Authors
            .Include(a => a.BookAuthors)
            .ThenInclude(ba => ba.Book)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Author?> UpdateAsync(Guid id, Author newAuthor, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var oldAuthor = await _context.Authors
            .Include(a => a.BookAuthors)
            .ThenInclude(ba => ba.Book)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (newAuthor == null) return null;

        oldAuthor.FirstName = newAuthor.FirstName;
        oldAuthor.LastName = newAuthor.LastName;

        oldAuthor.BookAuthors = newAuthor.BookAuthors.Select(ba => new BookAuthor
        {
            AuthorId = oldAuthor.Id,
            BookId = ba.BookId
        }).ToList();

        await _context.SaveChangesAsync(cancellationToken);

        return oldAuthor;
    }

    public async Task<Author?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var author = await _context.Authors
            .Include(a => a.BookAuthors)
            .ThenInclude(ba => ba.Book)
            .FirstOrDefaultAsync(author => author.Id == id);

        if (author == null) return null;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync(cancellationToken);

        return author;
    }

    public async Task<Author?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }
}