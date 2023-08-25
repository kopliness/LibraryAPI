using Library.Domain.Dto;
using Library.Domain.Models;

namespace Library.Database.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<BookModel?> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default);
        List<BookModel> GetBooks();
        Task<BookDto?> GetBookAsync(Guid? id = null, string? isbn = null, CancellationToken cancellationToken = default);
        Task<BookDto?> UpdateBookAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default);
        Task<BookDto?> DeleteBookAsync(Guid? id = null, string? isbn = null, CancellationToken cancellationToken = default);
    }
}
