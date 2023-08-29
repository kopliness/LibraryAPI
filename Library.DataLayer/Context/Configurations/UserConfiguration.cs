using Library.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataLayer.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
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
