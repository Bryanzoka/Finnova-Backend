using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BankAccountAPI.Data.Map
{
    public class BankClientMap : IEntityTypeConfiguration<BankClientModel>
    {
        public void Configure(EntityTypeBuilder<BankClientModel> builder)
        {
            builder
                .HasKey(x => x.CPF);

            builder
                .Property(x => x.CPF)
                .IsRequired()
                .HasColumnType("VARCHAR(11)")
                .HasColumnName("client_cpf");

            builder
                .Property(x => x.ClientName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("client_name");

            builder
                .Property(x => x.ClientEmail)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("client_email");

            builder
                .Property(x => x.ClientTel)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("client_tel");

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasColumnName("client_password");

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