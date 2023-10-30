using Library.DataLayer.Context.Configurations;
using Library.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DataLayer.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options){}

        public DbSet<BookModel> Books { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }
        
        public DbSet<BookAuthor> BookAuthors { get; set; }
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryContext).Assembly);
        }
    }
}