using System.Text.Json;
using BankApp.Data;
using BankApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services;

public class ExchangeRatesService : IHostedService, IDisposable
{
    private readonly ILogger<ExchangeRatesService> _logger;

    private readonly IServiceProvider _serviceProvider;
    private int _executionCount;
    private Timer? _timer;

    public ExchangeRatesService(ILogger<ExchangeRatesService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        GC.SuppressFinalize(this);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Exchange Rates Service running");
        _timer = new Timer(async _ => await DoWork(), null, TimeSpan.Zero, TimeSpan.FromHours(12));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Exchange Rates Service is stopping");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private async Task DoWork()
    {
        var count = Interlocked.Increment(ref _executionCount);
        _logger.LogInformation("Exchange Rates Service is working. Count: {Count}", count);

        const string url = "https://api.nbp.pl/api/exchangerates/tables/c";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();
        var jsonDocument = JsonDocument.Parse(jsonString);
        var rates = jsonDocument.RootElement[0].GetProperty("rates");

        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var plnCurrency = await dbContext.Currencies.SingleAsync(c => c.Code.Equals("PLN"));
            plnCurrency.Ask = 1;
            plnCurrency.Bid = 1;

            foreach (var rate in rates.EnumerateArray())
            {
                var currency = new Currency
                {
                    Code = rate.GetProperty("code").ToString(),
                    Ask = rate.GetProperty("ask").GetDecimal(),
                    Bid = rate.GetProperty("bid").GetDecimal()
                };

                var entity = await dbContext.Currencies.SingleAsync(c => c.Code.Equals(currency.Code));
                entity.Ask = currency.Ask;
                entity.Bid = currency.Bid;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}