using Microsoft.EntityFrameworkCore;
using FinnovaAPI.Models;
using FinnovaAPI.Data.Map;

namespace FinnovaAPI.Data
{
    public class FinnovaDbContext : DbContext
    {
        public FinnovaDbContext(DbContextOptions<FinnovaDbContext> options) : base(options)
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