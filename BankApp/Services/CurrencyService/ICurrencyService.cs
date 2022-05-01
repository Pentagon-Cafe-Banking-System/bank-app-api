using BankApp.Entities;

namespace BankApp.Services.CurrencyService;

public interface ICurrencyService
{
    Task<IList<Currency>> GetAllCurrenciesAsync(CancellationToken cancellationToken = default);
    Task<Currency> GetCurrencyByIdAsync(int currencyId, CancellationToken cancellationToken = default);
    Task<bool> CurrencyExistsByIdAsync(int currencyId, CancellationToken cancellationToken = default);
}