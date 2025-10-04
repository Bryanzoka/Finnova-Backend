using Finnova.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finnova.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts");

            builder.HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasColumnName("id");

            builder
                .Property(a => a.ClientId)
                .IsRequired()
                .HasColumnName("client_id");

            builder
                .Property(a => a.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("type");

            builder
                .Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("status");

            builder
                .Property(a => a.Balance)
                .IsRequired()
                .HasColumnType("DECIMAL(12,2)")
                .HasColumnName("balance");

            builder
                .Property(a => a.Password)
                .IsRequired()
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("password");

            builder
                .Property(a => a.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder
                .Property(a => a.UpdatedAt)
                .HasColumnName("updated_at");

            builder
                .Property(a => a.ClientId)
                .HasColumnName("client_id");

            builder
                .HasIndex(a => new { a.ClientId, a.Type })
                .IsUnique();

            builder
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(a => a.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}