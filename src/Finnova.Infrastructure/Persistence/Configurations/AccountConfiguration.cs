using Finnova.Domain.Entities;
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
                .Property(a => a.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            builder
                .Property(a => a.Type)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnName("type");

            builder
                .Property(a => a.IsActive)
                .IsRequired()
                .HasColumnName("is_active");

            builder
                .Property(a => a.Balance)
                .IsRequired()
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("balance");

            builder
                .Property(a => a.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder
                .Property(a => a.UpdatedAt)
                .HasColumnName("updated_at");

            builder
                .Property(a => a.UserId)
                .HasColumnName("user_id");

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}