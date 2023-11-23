using Library.Business.Dto;

namespace Library.Business.Services.Interfaces;

public interface IAuthorService
{
    Task<List<AuthorReadDto>> GetAuthorsAsync();

    Task<AuthorCreateDto> AddAuthorAsync(AuthorCreateDto authorCreateDto,
        CancellationToken cancellationToken = default);

    Task<AuthorCreateDto?> UpdateAuthorAsync(Guid id, AuthorCreateDto authorCreateDto,
        CancellationToken cancellationToken = default);

    Task<AuthorReadDto?> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken = default);
}