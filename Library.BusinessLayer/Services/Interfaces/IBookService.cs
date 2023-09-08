using Library.DataLayer.Models.Dto;
using Library.DataLayer.Models;

namespace Library.BusinessLayer.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookModel?> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default);
        List<BookModel> GetBooks();

        Task<BookDto?> GetBookAsyncById(Guid id, CancellationToken cancellationToken = default);
        Task<BookDto?> GetBookAsyncByIsbn(string isbn, CancellationToken cancellationToken = default);

        Task<BookDto?> UpdateBookAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default);
        Task<BookDto?> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default);
    }
}