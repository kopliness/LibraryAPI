using Library.Business.Dto;

namespace Library.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookCreateDto> AddBookAsync(BookCreateDto bookCreateDto, CancellationToken cancellationToken = default);
        Task<List<BookReadDto>> GetBooksAsync();

        Task<BookReadDto?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BookReadDto?> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken = default);

        Task<BookCreateDto?> UpdateBookAsync(Guid id, BookCreateDto bookCreateDto, CancellationToken cancellationToken = default);
        Task<BookReadDto?> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default);
    }
}