using Library.Database.Context.Configurations;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Database.Context
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) =>Database.EnsureCreated();

        public DbSet<BookModel> Books { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
