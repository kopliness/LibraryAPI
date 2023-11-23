using Library.DAL.Entities;

namespace Library.DAL.Repository.Interfaces;

public interface IAuthorRepository
{
    Task<List<Author>> ReadAllAsync();

    Task<Author?> CreateAsync(Author newAuthor, CancellationToken cancellationToken = default);
    Task<Author?> UpdateAsync(Guid id, Author newAuthor, CancellationToken cancellationToken = default);
    Task<Author?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Author?> ReadAsync(Guid id, CancellationToken cancellationToken = default);
}