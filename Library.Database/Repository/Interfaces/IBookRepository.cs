using Library.Domain.Dto;
using Library.Domain.Models;

namespace Library.Database.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<BookModel?> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default);
        List<BookModel> GetBooks();
        Task<BookDto?> GetBookByIdAsync(Guid? id = null, CancellationToken cancellationToken = default);
        Task<BookDto?> GetBookByISBNAsync(string? isbn = null, CancellationToken cancellationToken = default);
        Task<BookDto?> UpdateBookAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default);
        Task<BookDto?> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
