using AutoMapper;
using Library.Business.Dto;
using Library.Business.Services.Interfaces;
using Library.Common.Exceptions;
using Library.DAL.Entities;
using Library.DAL.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Business.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BookService> _logger;
    private readonly IMapper _mapper;


    public BookService(IBookRepository bookRepository, IMapper mapper, ILogger<BookService> logger)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookCreateDto> AddBookAsync(BookCreateDto bookCreateDto,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding a new book with ISBN: {Isbn}", bookCreateDto.Isbn);

        var book = _mapper.Map<Book>(bookCreateDto);
        var bookExists = await _bookRepository.ReadByIsbnAsync(bookCreateDto.Isbn, cancellationToken);

        if (bookExists != null)
        {
            _logger.LogError("A book with this ISBN already exists.");
            throw new ExistsException("A book with this ISBN already exists.");
        }

        foreach (var authorId in bookCreateDto.Authors)
            if (!await _bookRepository.AuthorExists(authorId))
            {
                _logger.LogError($"Author with ID {authorId} not found.");
                throw new NotFoundException($"Author with ID {authorId} not found.");
            }

        var createdBook = await _bookRepository.CreateAsync(book, cancellationToken);

        await _bookRepository.AddAuthorToBook(createdBook.Id, bookCreateDto.Authors);

        _logger.LogInformation("Added a new book with Id: {Id}", createdBook.Id);

        return _mapper.Map<Book, BookCreateDto>(createdBook);
    }

    public async Task<BookReadDto?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting a book with Id: {id}", id);

        var book = await _bookRepository.ReadByIdAsync(id, cancellationToken);

        if (book == null)
        {
            _logger.LogError($"Book with ID {id} not found.", id);
            throw new NotFoundException("Book with this id not found.", id);
        }

        _logger.LogInformation("The book was obtained with Id: {id}", id);

        return _mapper.Map<Book, BookReadDto>(book);
    }

    public async Task<BookReadDto?> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting a book with Isbn: {isbn}", isbn);

        var book = await _bookRepository.ReadByIsbnAsync(isbn, cancellationToken);

        if (book == null)
        {
            _logger.LogError($"Book with ISBN {isbn} not found.");
            throw new NotFoundException("Book with this ISBN not found.", isbn);
        }

        _logger.LogInformation("The book was obtained with ISBN: {isbn}", isbn);

        return _mapper.Map<Book, BookReadDto>(book);
    }

    public async Task<List<BookReadDto>> GetBooksAsync()
    {
        _logger.LogInformation("Getting the whole list of books.");

        var books = await _bookRepository.ReadAllAsync();

        _logger.LogInformation("Retrieving the entire list of books was successful.");

        return _mapper.Map<List<BookReadDto>>(books);
    }

    public async Task<BookCreateDto?> UpdateBookAsync(Guid id, BookCreateDto bookCreateDto,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Updating book with id: {id}", id);

        var book = await _bookRepository.ReadByIdAsync(id, cancellationToken);

        if (book == null)
        {
            _logger.LogError($"Book with ID {id} not found.", id);
            throw new NotFoundException("Book with this id not found.", id);
        }

        var bookExists = await _bookRepository.ReadByIsbnAsync(bookCreateDto.Isbn, cancellationToken);

        if (bookExists != null)
        {
            _logger.LogError($"A book with this ISBN {bookCreateDto.Isbn} already exists.");
            throw new ExistsException($"A book with this ISBN {bookCreateDto.Isbn} already exists.");
        }

        foreach (var authorId in bookCreateDto.Authors)
            if (!await _bookRepository.AuthorExists(authorId))
            {
                _logger.LogError($"Author with ID {authorId} not found.");
                throw new NotFoundException($"Author with ID {authorId} not found.");
            }

        var bookModel = _mapper.Map<BookCreateDto, Book>(bookCreateDto);
        var updatedBook = await _bookRepository.UpdateAsync(id, bookModel, cancellationToken);

        await _bookRepository.AddAuthorToBook(updatedBook.Id, bookCreateDto.Authors);

        _logger.LogInformation("Book with this id has been updated: {Id}", updatedBook.Id);

        return _mapper.Map<Book, BookCreateDto>(updatedBook);
    }

    public async Task<BookReadDto?> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting a book with Id : {id}", id);

        var book = await _bookRepository.DeleteAsync(id, cancellationToken);

        if (book == null)
        {
            _logger.LogError($"Book with ID {id} not found.", id);
            throw new NotFoundException("Book with this id not found.", id);
        }

        _logger.LogInformation("The removal of the book was successful with Id : {id}", id);

        return _mapper.Map<Book, BookReadDto>(book);
    }
}