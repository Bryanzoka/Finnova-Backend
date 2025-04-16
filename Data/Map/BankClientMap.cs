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
            builder.HasKey(x => x.CPF);
            builder.Property(x => x.ClientName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ClientEmail).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ClientTel).IsRequired().HasMaxLength(20);
        }
    }
}