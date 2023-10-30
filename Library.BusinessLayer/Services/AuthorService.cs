using AutoMapper;
using Library.BusinessLayer.Dto;
using Library.BusinessLayer.Services.Interfaces;
using Library.DataLayer.Models;
using Library.DataLayer.Repository.Interfaces;
using Library.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace Library.BusinessLayer.Services;

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
    
    public List<AuthorReadDto> GetAuthors()
    {
        _logger.LogInformation("Getting the whole list of authors.");

        var authors = _authorRepository.ReadAll();
        _logger.LogInformation("Retrieving the entire list of authors was successful.");

        return _mapper.Map<List<AuthorReadDto>>(authors);
    }
    
    public async Task<AuthorCreateDto> AddAuthorAsync(AuthorCreateDto authorCreateDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("The author's addition has begun.");

        var author = _mapper.Map<AuthorModel>(authorCreateDto);

        var createdAuthor = await _authorRepository.CreateAsync(author, cancellationToken);

        _logger.LogInformation("Added a new author with Id: {Id}", createdAuthor.Id);

        
        return _mapper.Map<AuthorModel, AuthorCreateDto>(createdAuthor);
    }
    public async Task<AuthorCreateDto?> UpdateAuthorAsync(Guid id, AuthorCreateDto authorCreateDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating author with id: {id}", id);

        var existingAuthor = await _authorRepository.ReadAsync(id, cancellationToken);

        if (existingAuthor == null)
        {
            _logger.LogError("Author with id {id} not found");
            throw new NotFoundException("Author with id {id} not found");
        }

        var authorModel = _mapper.Map<AuthorModel>(authorCreateDto);
        var updatedAuthor = await _authorRepository.UpdateAsync(id, authorModel, cancellationToken);

        _logger.LogInformation("Author with this id has been updated: {id}", id);
        
        return _mapper.Map<AuthorModel, AuthorCreateDto>(updatedAuthor);
    }

    public async Task<AuthorReadDto?> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting a author with Id : {id}", id);

        var author = await _authorRepository.DeleteAsync(id, cancellationToken);

        if (author == null)
        {
            _logger.LogError("Author with ID {id} not found.");
            throw new NotFoundException("Author not found");
        }
        
        _logger.LogInformation("The removal of the author was successful with Id : {id}", id);
        return _mapper.Map<AuthorModel, AuthorReadDto>(author);
    }
}