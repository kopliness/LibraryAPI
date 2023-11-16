using Library.Business.Dto;
using Library.DAL.Models;

namespace Library.Business.Services.Interfaces;

public interface IAuthorService
{
    List<AuthorReadDto> GetAuthors();
    
    Task<AuthorCreateDto> AddAuthorAsync(AuthorCreateDto authorCreateDto, CancellationToken cancellationToken = default);

    Task<AuthorCreateDto?> UpdateAuthorAsync(Guid id, AuthorCreateDto authorCreateDto,
        CancellationToken cancellationToken = default);

    Task<AuthorReadDto?> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken = default);
}