using Library.DataLayer.Models.Dto;
using Library.DataLayer.Models;

namespace Library.BusinessLayer.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookModel?> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default);
        List<BookModel> GetBooks();
        Task<BookDto?> GetBookAsync(Guid? id = null, string? isbn = null, CancellationToken cancellationToken = default);
        Task<BookDto?> UpdateBookAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default);
        Task<BookDto?> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
