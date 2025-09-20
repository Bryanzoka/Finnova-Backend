using Finnova.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finnova.Infrastructure.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");

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
                .Property(x => x.Cpf)
                .IsRequired()
                .HasColumnType("VARCHAR(11)")
                .IsFixedLength()
                .HasColumnName("cpf");

            builder
                .HasIndex(x => x.Cpf)
                .IsUnique();

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("email");

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("phone");

            builder
                .HasIndex(x => x.Phone)
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