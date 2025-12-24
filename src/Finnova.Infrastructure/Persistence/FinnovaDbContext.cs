using Finnova.Domain.Entities;
using Finnova.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Finnova.Infrastructure.Persistence
{
    public class FinnovaDbContext : DbContext
    {
        public FinnovaDbContext(DbContextOptions<FinnovaDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new VerificationCodeConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}