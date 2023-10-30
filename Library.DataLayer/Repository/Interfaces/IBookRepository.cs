using Library.DataLayer.Models;

namespace Library.DataLayer.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<BookModel?> CreateAsync(BookModel bookModel, CancellationToken cancellationToken = default);
        List<BookModel> ReadAll();
        Task<BookModel?> ReadByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BookModel?> ReadByIsbnAsync(string isbn, CancellationToken cancellationToken = default);
        Task<BookModel?> UpdateAsync(Guid id, BookModel bookModel, CancellationToken cancellationToken = default);
        Task<BookModel?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAuthorToBook(Guid bookId, List<Guid> authorIds, CancellationToken cancellationToken = default);
        Task<bool> AuthorExists(Guid authorId);
    }
}