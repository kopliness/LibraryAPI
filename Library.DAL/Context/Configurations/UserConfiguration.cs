using Library.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DAL.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(login => login.Login);

            builder.Property(login => login.Login)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(password => password.Password)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}