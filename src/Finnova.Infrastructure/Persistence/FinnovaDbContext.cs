using Finnova.Domain.Aggregates;
using Finnova.Infrastructure.Configurations;
using Finnova.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Finnova.Infrastructure.Persistence
{
    public class FinnovaDbContext : DbContext
    {
        public FinnovaDbContext(DbContextOptions<FinnovaDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new VerificationCodeConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}