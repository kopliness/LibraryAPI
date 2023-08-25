using AutoMapper;
using Library.Database.Context;
using Library.Database.Repository.Interfaces;
using Library.Domain.Dto;
using Library.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Library.Database.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        private readonly ILogger<BookRepository> _logger;
        private readonly IMapper _mapper;
        public Task<BookModel?> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto?> DeleteBookAsync(Guid? id = null, string? isbn = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto?> GetBookAsync(Guid? id = null, string? isbn = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<BookModel> GetBooks()
        {
            throw new NotImplementedException();
        }

        public Task<BookDto?> UpdateBookAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
