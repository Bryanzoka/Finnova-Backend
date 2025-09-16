using FinnovaAPI.Data;
using FinnovaAPI.Enums;

namespace FinnovaAPI.Services
{
    public class SavingsYieldService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;
        private const decimal YieldRate = 0.02m;

        public SavingsYieldService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
    
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ApplyYield, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        private void ApplyYield(object state)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<FinnovaDbContext>();
                    var savingsAccounts = context.BankAccount
                    .Where(x => x.AccountType == EnumAccountType.Savings && x.Balance < 99999999.99m)
                    .ToList();

                    foreach (var account in savingsAccounts)
                    {
                        account.YieldAccount(account.Balance * YieldRate);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying yield: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}