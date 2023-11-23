using AutoMapper;
using Library.Business.Dto;
using Library.Business.Services.Interfaces;
using Library.DAL.Repository.Interfaces;
using Library.Common.Exceptions;
using Library.DAL.Entities;
using Microsoft.Extensions.Logging;

namespace Library.Business.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthorService> _logger;

    
    public AuthorService(IAuthorRepository authorRepository, IMapper mapper,ILogger<AuthorService> logger)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<AuthorReadDto>> GetAuthorsAsync()
    {
        _logger.LogInformation("Getting the whole list of authors.");

        var authors = await _authorRepository.ReadAllAsync();
        _logger.LogInformation("Retrieving the entire list of authors was successful.");

        return _mapper.Map<List<AuthorReadDto>>(authors);
    }
    
    public async Task<AuthorCreateDto> AddAuthorAsync(AuthorCreateDto authorCreateDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("The author's addition has begun.");

        var author = _mapper.Map<Author>(authorCreateDto);

        var createdAuthor = await _authorRepository.CreateAsync(author, cancellationToken);

        _logger.LogInformation("Added a new author with Id: {Id}", createdAuthor.Id);

        
        return _mapper.Map<Author, AuthorCreateDto>(createdAuthor);
    }
    public async Task<AuthorCreateDto?> UpdateAuthorAsync(Guid id, AuthorCreateDto authorCreateDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating author with id: {id}", id);

        var existingAuthor = await _authorRepository.ReadAsync(id, cancellationToken);

        if (existingAuthor == null)
        {
            _logger.LogError($"Author with ID {id} not found.", id);
            throw new NotFoundException("Author with this id not found.", id);
        }

        var authorModel = _mapper.Map<Author>(authorCreateDto);
        var updatedAuthor = await _authorRepository.UpdateAsync(id, authorModel, cancellationToken);

        _logger.LogInformation("Author with this id has been updated: {id}", id);
        
        return _mapper.Map<Author, AuthorCreateDto>(updatedAuthor);
    }

    public async Task<AuthorReadDto?> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting a author with Id : {id}", id);

        var author = await _authorRepository.DeleteAsync(id, cancellationToken);

        if (author == null)
        {
            _logger.LogError($"Author with ID {id} not found.", id);
            throw new NotFoundException("Author with this id not found.", id);
        }
        
        _logger.LogInformation("The removal of the author was successful with Id : {id}", id);
        return _mapper.Map<Author, AuthorReadDto>(author);
    }
}