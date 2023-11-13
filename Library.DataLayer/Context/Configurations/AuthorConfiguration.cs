using Library.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataLayer.Context.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorModel>
    {
        public void Configure(EntityTypeBuilder<AuthorModel> builder)
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
}
