using Library.DAL.Context;
using Library.DAL.Entities;
using Library.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Library.DAL.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) => _context = context;

        public async Task<Book?> CreateAsync(Book newBook, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var book = await _context.AddAsync(newBook, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return book.Entity;
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

        public async Task<List<Book>> ReadAllAsync()
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<Book?> UpdateAsync(Guid id, Book newBook,
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


            book.Isbn = newBook.Isbn;
            book.Title = newBook.Title;
            book.Genre = newBook.Genre;
            book.Description = newBook.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return book;
        }

        public async Task<Book?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

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