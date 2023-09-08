using AutoMapper;
using Library.DataLayer.Context;
using Library.Shared.Exceptions;
using Library.DataLayer.Repository.Interfaces;
using Library.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using Library.DataLayer.Models.Dto;

namespace Library.DataLayer.Repository
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

        public async Task<BookModel?> CreateAsync(BookDto bookDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _context.Books.AsNoTracking()
                .FirstOrDefaultAsync(isbn => isbn.Isbn == bookDto.Isbn);

            if (book != null)
            {
                throw new BookExistsException("Книга с таким ISBN уже существует.");
            }

            book = _mapper.Map<BookModel>(bookDto);
            var entity = await _context.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Entity;
        }

        public async Task<BookDto?> ReadAsyncById(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var books = _context.Books;

            var book = await _context.Books.FirstOrDefaultAsync(b=>b.Id == id);

            return _mapper.Map<BookDto>(book);
        }
        public async Task<BookDto?> ReadAsyncByIsbn(string isbn, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var books = _context.Books;

            var book = await _context.Books.FirstOrDefaultAsync(b=>b.Isbn == isbn);

            return _mapper.Map<BookDto>(book);
        }

        public List<BookModel> ReadAll() => _context.Books.AsNoTracking().ToList();

        public async Task<BookDto?> UpdateAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            _mapper.Map(bookDto, book);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var books = _context.Books;
            var book = books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return null;
            }

            var entity = _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Книга с Id {entity.Entity.Id} была удалена");

            return _mapper.Map<BookDto>(book);
        }
    }
}