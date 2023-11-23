using Library.DAL.Entities;

namespace Library.DAL.Repository.Interfaces;

public interface IBookRepository
{
    Task<Book?> CreateAsync(Book newBook, CancellationToken cancellationToken = default);
    Task<List<Book>> ReadAllAsync();
    Task<Book?> ReadByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Book?> ReadByIsbnAsync(string isbn, CancellationToken cancellationToken = default);
    Task<Book?> UpdateAsync(Guid id, Book newBook, CancellationToken cancellationToken = default);
    Task<Book?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAuthorToBook(Guid bookId, List<Guid> authorIds, CancellationToken cancellationToken = default);
    Task<bool> AuthorExists(Guid authorId);
}