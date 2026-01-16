using Finnova.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finnova.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(t => t.Id);

            builder
                .Property(t => t.Id)
                .HasColumnName("id");

            builder
                .Property(t => t.AccountId)
                .IsRequired()
                .HasColumnName("account_id");

            builder
                .Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("amount");

            builder
                .Property(t => t.Type)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnName("type");

            builder
                .Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(255)
                .HasColumnName("description");
            
            builder
                .Property(t => t.Date)
                .IsRequired()
                .HasColumnName("date");

            builder
                .Property(t => t.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder
                .HasOne<Account>()
                .WithMany()
                .HasForeignKey(t => t.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}