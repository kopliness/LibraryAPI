using Library.DAL.Models;

namespace Library.DAL.Repository.Interfaces;

public interface IAuthorRepository
{
    List<Author> ReadAll();
    
    Task<Author?> CreateAsync(Author author, CancellationToken cancellationToken = default);
    Task<Author?> UpdateAsync(Guid id, Author author, CancellationToken cancellationToken = default);
    Task<Author?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Author?> ReadAsync(Guid id, CancellationToken cancellationToken = default);
}