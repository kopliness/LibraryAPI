using Library.Business.Dto;
using Library.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.API.Controllers;

[ApiController]
[Authorize]
[Route("book")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet]
    [SwaggerOperation(Summary = "Get all books", Description = "Get a list of all books")]
    [SwaggerResponse(200, "Returns a list of BookReadDto", typeof(List<BookReadDto>))]
    [SwaggerResponse(401, "If account is not authorized")]
    [SwaggerResponse(500, "If there is an internal server error")]
    public async Task<IActionResult> GetBooks()
    {
        return Ok(await _bookService.GetBooksAsync());
    }

    [SwaggerOperation(Summary = "Get a book by ID", Description = "Get a specific book by ID")]
    [SwaggerResponse(200, "Returns a book with the specified ID", typeof(BookReadDto))]
    [SwaggerResponse(401, "If account is not authorized")]
    [SwaggerResponse(404, "If a book with the specified ID is not found")]
    [SwaggerResponse(500, "If there is an internal server error")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBookById(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var book = await _bookService.GetBookByIdAsync(id, cancellationToken);

        return Ok(book);
    }

    [SwaggerOperation(Summary = "Get a book by ISBN", Description = "Get a specific book by ISBN")]
    [SwaggerResponse(200, "Returns a book with the specified ISBN", typeof(BookReadDto))]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "If account is not authorized")]
    [SwaggerResponse(404, "If a book with the specified ISBN is not found")]
    [SwaggerResponse(500, "If there is an internal server error")]
    [HttpGet("{isbn}")]
    public async Task<IActionResult> GetBookByIsbn(string isbn, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var book = await _bookService.GetBookByIsbnAsync(isbn, cancellationToken);

        return Ok(book);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Add a new book", Description = "Add a new book")]
    [SwaggerResponse(201, "Returns the newly added book", typeof(BookCreateDto))]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "If account is not authorized")]
    [SwaggerResponse(500, "If there is an internal server error")]
    public async Task<IActionResult> AddBook(BookCreateDto bookCreateDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var book = await _bookService.AddBookAsync(bookCreateDto, cancellationToken);

        return Created($"/api/books/{book}", book);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update a book", Description = "Update a specific book(delete all authors)")]
    [SwaggerResponse(200, "Returns the updated book", typeof(BookCreateDto))]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "If account is not authorized")]
    [SwaggerResponse(404, "If a book with the specified ID is not found")]
    [SwaggerResponse(500, "If there is an internal server error")]
    public async Task<IActionResult> UpdateBook(Guid id, BookCreateDto bookCreateDto,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var book = await _bookService.UpdateBookAsync(id, bookCreateDto, cancellationToken);

        return Ok(book);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a book", Description = "Delete a specific book")]
    [SwaggerResponse(200, "Book deleted successfully")]
    [SwaggerResponse(401, "If account is not authorized")]
    [SwaggerResponse(404, "If a book with the specified ID is not found")]
    [SwaggerResponse(500, "If there is an internal server error")]
    public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var book = await _bookService.DeleteBookAsync(id, cancellationToken);

        return Ok(book);
    }
}