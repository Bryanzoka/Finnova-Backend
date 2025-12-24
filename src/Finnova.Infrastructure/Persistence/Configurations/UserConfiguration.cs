using Finnova.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finnova.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("name");

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("email");

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("VARCHAR(255)");

            builder
                .Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder
                .Property(x => x.UpdatedAt)
                .IsRequired()
                .HasColumnName("updated_at");
        }
    }
}