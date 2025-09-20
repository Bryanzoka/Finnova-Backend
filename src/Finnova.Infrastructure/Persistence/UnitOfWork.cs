using Finnova.Application.Contracts;

namespace Finnova.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinnovaDbContext _dbContext;

        public UnitOfWork(FinnovaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}