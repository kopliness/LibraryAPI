using Library.Database.Repository.Interfaces;
using Library.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)=>_bookRepository = bookRepository;

        [HttpGet("/getBooks")]
        public async Task<IActionResult> GetBooks()=>Ok(_bookRepository.GetBooks());

        [HttpGet("/getBookById")]
        public async Task<IActionResult> GetBookById(Guid? id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookRepository.GetBookByIdAsync(id, cancellationToken);

            if(book == null)
            {
                return NotFound("Такой книги нет в библиотеке.");
            }
            
            return Ok(book);
        }
        [HttpGet("/getBookByISBN")]
        public async Task<IActionResult> GetBookByISBN(string? isbn, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookRepository.GetBookByISBNAsync(isbn, cancellationToken);

            if (book == null)
            {
                return NotFound("Такой книги нет в библиотеке.");
            }

            return Ok(book);
        }
        [HttpPost("/addBook")]
        public async Task<IActionResult> AddBook(BookDto bookDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookRepository.AddBookAsync(bookDto, cancellationToken);

            return Ok(book);
        }
        [HttpPut("/updateBook")]
        public async Task<IActionResult> UpdateBook([FromQuery] Guid id, BookDto bookDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookRepository.UpdateBookAsync(id, bookDto, cancellationToken);

            if(book == null)
            {
                return NotFound("Книга не найдена");
            }

            return Ok(book);
        }
        [HttpDelete("/deleteBook")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _bookRepository.DeleteBookAsync(id, cancellationToken);

            if(book == null)
            {
                return NotFound("Книга не найдена");
            }

            return Ok(book);
        }

    }
}
