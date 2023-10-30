using Library.DataLayer.Context;
using Library.DataLayer.Repository.Interfaces;
using Library.DataLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace Library.DataLayer.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) => _context = context;

        public async Task<BookModel?> CreateAsync(BookModel bookModel, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var book = await _context.AddAsync(bookModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return book.Entity;
        }


        public async Task<BookModel?> ReadByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            
            return book;
        }

        public async Task<BookModel?> ReadByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _context.Books
                .Include(b=>b.BookAuthors)
                .ThenInclude(ba=>ba.Author)
                .FirstOrDefaultAsync(b => b.Isbn == isbn);

            return book;
        }

        public List<BookModel> ReadAll()
        {
            return _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .AsNoTracking()
                .ToList();
        }


        public async Task<BookModel?> UpdateAsync(Guid id, BookModel bookModel,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null;
            }
            
            var existingBookWithSameIsbn =
                await _context.Books.FirstOrDefaultAsync(b => b.Isbn == book.Isbn, cancellationToken);
            if (existingBookWithSameIsbn != null && existingBookWithSameIsbn.Id != id)
            {
                return null;
            }
            _context.BookAuthors.RemoveRange(book.BookAuthors);


            book.Isbn = bookModel.Isbn;
            book.Title = bookModel.Title;
            book.Genre = bookModel.Genre;
            book.Description = bookModel.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return book;
        }

        public async Task<BookModel?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
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