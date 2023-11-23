using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DAL.Context.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(key => key.Id);

        builder.Property(firstName => firstName.FirstName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(lastName => lastName.LastName)
            .IsRequired()
            .HasMaxLength(30);
    }
}