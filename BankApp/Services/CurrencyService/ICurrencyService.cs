using BankApp.Entities;

namespace BankApp.Services.CurrencyService;

public interface ICurrencyService
{
    Task<IList<Currency>> GetAllCurrenciesAsync(CancellationToken cancellationToken = default);
    Task<Currency> GetCurrencyByIdAsync(short currencyId, CancellationToken cancellationToken = default);
    Task<bool> CurrencyExistsByIdAsync(short currencyId, CancellationToken cancellationToken = default);
}