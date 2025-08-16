using FinnovaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FinnovaAPI.Data.Map
{
    public class BankClientMap : IEntityTypeConfiguration<BankClientModel>
    {
        public void Configure(EntityTypeBuilder<BankClientModel> builder)
        {
            builder.ToTable("clients");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.Cpf)
                .IsRequired()
                .HasColumnType("VARCHAR(11)")
                .HasColumnName("cpf");

            builder
                .HasIndex(x => x.Cpf)
                .IsUnique();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255)
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
                .HasColumnName("password");

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