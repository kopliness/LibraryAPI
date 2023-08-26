using AutoMapper;
using Library.Database.Context;
using Library.Database.Exceptions;
using Library.Database.Repository.Interfaces;
using Library.Domain.Dto;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Library.Mapper.Extensions;
using System.Linq;

namespace Library.Database.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        private readonly ILogger<BookRepository> _logger;

        private readonly IMapper _mapper;

        public BookRepository(LibraryContext context, IMapper mapper, ILogger<BookRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BookModel?> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book=_context.Books.AsNoTracking()
                .FirstOrDefault(isbn=>isbn.Isbn==bookDto.Isbn);

            if (book != null)
            {
                throw new BookExistsException("Книга с таким ISBN уже существует.");
            }

            book = bookDto.MapToModel(_mapper);
            var entity = await _context.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Entity;
        }

        public async Task<BookDto?> GetBookByIdAsync(Guid? id = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var books = _context.Books;

            var book = await books.FirstOrDefaultAsync(b => b.Id == id);

            return book?.MapToDto(_mapper);
        }
        public async Task<BookDto?> GetBookByISBNAsync(string? isbn = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var books = _context.Books;

            var book = await books.FirstOrDefaultAsync(b => b.Isbn == isbn);

            return book?.MapToDto(_mapper);
        }

        public List<BookModel> GetBooks() => _context.Books.AsNoTracking().ToList();

        public async Task<BookDto?> UpdateBookAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _context.Books.FirstOrDefaultAsync(b=>b.Id==id);

            if (book == null)
            {
                return null;
            }
            _mapper.Map(bookDto, book);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BookDto>(book);
        } 

        public async Task<BookDto?> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var books = _context.Books;
            var book = books.FirstOrDefault(book => book.Id == id);

            if( book == null )
            {
                return null;
            }

            var entity = _context.Books.Remove( book );
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Книга с Id {entity.Entity.Id} была удалена");

            return book.MapToDto(_mapper);
        }


    }
}
