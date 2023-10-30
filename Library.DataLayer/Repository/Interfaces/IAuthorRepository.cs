using Library.DataLayer.Models;

namespace Library.DataLayer.Repository.Interfaces;

public interface IAuthorRepository
{
    List<AuthorModel> ReadAll();
    
    Task<AuthorModel?> CreateAsync(AuthorModel authorModel, CancellationToken cancellationToken = default);
    Task<AuthorModel?> UpdateAsync(Guid id, AuthorModel authorModel, CancellationToken cancellationToken = default);
    Task<AuthorModel?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<AuthorModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default);
}