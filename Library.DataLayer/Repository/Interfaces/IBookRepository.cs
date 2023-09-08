using Library.DataLayer.Models.Dto;
using Library.DataLayer.Models;

namespace Library.DataLayer.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<BookModel?> CreateAsync(BookDto bookDto, CancellationToken cancellationToken = default);
        List<BookModel> ReadAll();
        Task<BookDto?> ReadAsyncById(Guid id, CancellationToken cancellationToken = default);
        Task<BookDto?> ReadAsyncByIsbn(string isbn, CancellationToken cancellationToken = default);
        Task<BookDto?> UpdateAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default);
        Task<BookDto?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}