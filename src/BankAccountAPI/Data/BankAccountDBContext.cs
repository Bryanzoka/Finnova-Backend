using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankAccountAPI.Models;
using BankAccountAPI.Data.Map;

namespace BankAccountAPI.Data
{
    public class BankAccountDBContext : DbContext
    {
        public BankAccountDBContext(DbContextOptions<BankAccountDBContext> options) : base(options)
        {

        }

        public DbSet<BankClientModel> BankClient { set; get; }
        public DbSet<BankAccountModel> BankAccount { set; get; }
        public DbSet<ClientVerificationCodeModel> ClientVerificationCode { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankClientMap());
            modelBuilder.ApplyConfiguration(new BankAccountMap());
            modelBuilder.ApplyConfiguration(new ClientValidationCodeMap());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}