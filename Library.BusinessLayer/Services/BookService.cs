using Library.BusinessLayer.Services.Interfaces;
using Library.Shared.Exceptions;
using Library.DataLayer.Models;
using Library.DataLayer.Models.Dto;
using Library.DataLayer.Repository.Interfaces;

namespace Library.BusinessLayer.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<BookModel?> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.CreateAsync(bookDto, cancellationToken);

            return book;
        }

        public async Task<BookDto?> GetBookAsync(Guid? id = null, string? isbn = null,
            CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.ReadAsync(id, isbn, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            return book;
        }

        public List<BookModel> GetBooks()
        {
            return _bookRepository.ReadAll();
        }

        public async Task<BookDto?> UpdateBookAsync(Guid id, BookDto bookDto,
            CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.UpdateAsync(id, bookDto, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            return book;
        }

        public async Task<BookDto?> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.DeleteAsync(id, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException("Book not foundмне ");
            }

            return book;
        }
    }
}