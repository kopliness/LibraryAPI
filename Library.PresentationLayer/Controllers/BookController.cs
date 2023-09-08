using Library.BusinessLayer.Services.Interfaces;
using Library.DataLayer.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.PresentationLayer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) => _bookService = bookService;

        [HttpGet]
        public async Task<IActionResult> GetBooks() => Ok(_bookService.GetBooks());

        [HttpGet("/getBook")]
        public async Task<IActionResult> GetBook(Guid? id, string? isbn, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookService.GetBookAsync(id, isbn, cancellationToken);

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto bookDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookService.AddBookAsync(bookDto, cancellationToken);

            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookDto bookDto,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookService.UpdateBookAsync(id, bookDto, cancellationToken);

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookService.DeleteBookAsync(id, cancellationToken);

            return Ok(book);
        }
    }
}