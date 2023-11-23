using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options){}

        public LibraryContext()
        {
            
        }
        public virtual DbSet<Book> Books { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Author> Authors { get; set; }
        
        public DbSet<BookAuthor> BookAuthors { get; set; }
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}