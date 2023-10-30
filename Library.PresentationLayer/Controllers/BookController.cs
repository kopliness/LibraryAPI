using Library.BusinessLayer.Dto;
using Library.BusinessLayer.Services.Interfaces;
using Library.BusinessLayer.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.PresentationLayer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        
        private readonly BookValidator _bookValidator;
        
        private readonly IsbnValidator _isbnValidator;

        public BookController(IBookService bookService, BookValidator bookValidator,IsbnValidator isbnValidator)
        {
            _bookService = bookService;
            _bookValidator = bookValidator;
            _isbnValidator = isbnValidator;
        }

        [HttpGet]
        public Task<IActionResult> GetBooks() => Task.FromResult<IActionResult>(Ok( _bookService.GetBooks()));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBookById(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookService.GetBookByIdAsync(id, cancellationToken);

            return Ok(book);
        }

        [HttpGet("{isbn}")]
        public async Task<IActionResult> GetBookByIsbn(string isbn, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validationResult = _isbnValidator.Validate(isbn);
  
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var book = await _bookService.GetBookByIsbnAsync(isbn, cancellationToken);

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookCreateDto bookCreateDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var validationResult = _bookValidator.Validate(bookCreateDto);
            
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var book = await _bookService.AddBookAsync(bookCreateDto, cancellationToken);

            return Ok(book);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookCreateDto bookCreateDto,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var validationResult = _bookValidator.Validate(bookCreateDto);
  
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var book = await _bookService.UpdateBookAsync(id, bookCreateDto, cancellationToken);

            return Ok(book);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookService.DeleteBookAsync(id, cancellationToken);

            return Ok(book);
        }
    }
}