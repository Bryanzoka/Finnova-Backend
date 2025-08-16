using FinnovaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FinnovaAPI.Data.Map
{
    public class BankAccountMap : IEntityTypeConfiguration<BankAccountModel>
    {
        public void Configure(EntityTypeBuilder<BankAccountModel> builder)
        {
            builder.ToTable("accounts");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.ClientId)
                .IsRequired()
                .HasColumnName("client_id");

            builder
                .HasIndex(x => new { x.ClientId, x.AccountType })
                .IsUnique();

            builder
                .Property(x => x.AccountType)
                .IsRequired()
                .HasColumnName("account_type");

            builder
                .Property(x => x.Balance)
                .IsRequired()
                .HasColumnName("balance")
                .HasColumnType("DECIMAL(10,2)");

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("VARCHAR(4)");

            builder
                .Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder
                .Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");

            builder
                .Property(x => x.LastTransactionAt)
                .HasColumnName("last_transaction_at");

            builder
                .HasOne(x => x.BankClient)
                .WithMany() 
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}