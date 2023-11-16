using Library.DAL.Context;
using Library.DAL.Repository.Interfaces;
using Library.DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace Library.DAL.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) => _context = context;

        public async Task<Book?> CreateAsync(Book book, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var books = await _context.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return books.Entity;
        }


        public async Task<Book?> ReadByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            
            return book;
        }

        public async Task<Book?> ReadByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _context.Books
                .Include(b=>b.BookAuthors)
                .ThenInclude(ba=>ba.Author)
                .FirstOrDefaultAsync(b => b.Isbn == isbn);

            return book;
        }

        public List<Book> ReadAll()
        {
            return _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .AsNoTracking()
                .ToList();
        }


        public async Task<Book?> UpdateAsync(Guid id, Book book,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var books = await _context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (books == null)
            {
                return null;
            }
            
            var existingBookWithSameIsbn =
                await _context.Books.FirstOrDefaultAsync(b => b.Isbn == books.Isbn, cancellationToken);
            if (existingBookWithSameIsbn != null && existingBookWithSameIsbn.Id != id)
            {
                return null;
            }
            _context.BookAuthors.RemoveRange(books.BookAuthors);


            books.Isbn = book.Isbn;
            books.Title = book.Title;
            books.Genre = book.Genre;
            books.Description = book.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return books;
        }

        public async Task<Book?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = _context.Books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return null;
            }

            _context.Books.Remove(book);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return book;
        }

        public async Task AddAuthorToBook(Guid bookId, List<Guid> authorIds, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _context.BookAuthors.AddRange(authorIds.Select(x => new BookAuthor
            {
                BookId = bookId,
                AuthorId = x
            }));
    
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> AuthorExists(Guid authorId)
        {
            return await _context.Authors.AnyAsync(a => a.Id == authorId);
        }
    }
}