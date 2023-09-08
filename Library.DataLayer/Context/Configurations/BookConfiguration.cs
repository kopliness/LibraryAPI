using Library.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataLayer.Context.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            builder.HasKey(key => key.Id);

            builder.Property(isbn => isbn.Isbn)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(title => title.Title)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(genre => genre.Genre)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(description => description.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(author => author.Author)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(borrowTime => borrowTime.BorrowTime)
                .HasDefaultValue(DateTime.Now);

            builder.Property(returnTime => returnTime.ReturnTime)
                .HasDefaultValue(DateTime.Now.AddDays(7));
        }
    }
}