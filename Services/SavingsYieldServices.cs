using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountAPI.Data;
using BankAccountAPI.Enums;

namespace BankAccountAPI.Services
{
    public class SavingsYieldServices : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;
        private const decimal YieldRate = 0.02m;

        public SavingsYieldServices(IServiceScopeFactory scopeFactory)
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
                    var context = scope.ServiceProvider.GetService<BankAccountDBContext>();
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
                Console.WriteLine($"Erro ao aplicar rendimento: {ex.Message}");
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