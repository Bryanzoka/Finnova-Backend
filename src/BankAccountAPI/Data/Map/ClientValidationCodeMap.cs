using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankAccountAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAccountAPI.Data.Map
{
    public class ClientValidationCodeMap : IEntityTypeConfiguration<ClientVerificationCodeModel>
    {
        public void Configure(EntityTypeBuilder<ClientVerificationCodeModel> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");

            builder
                .Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(6)
                .HasColumnName("code");

            builder
                .Property(x => x.Expiration)
                .IsRequired()
                .HasColumnName("expiration");
        }
    }
}