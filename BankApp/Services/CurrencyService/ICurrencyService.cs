using BankApp.Entities;

namespace BankApp.Services.CurrencyService;

public interface ICurrencyService
{
    Task<IList<Currency>> GetAllCurrenciesAsync();
    Task<Currency> GetCurrencyByIdAsync(short currencyId);
    Task<bool> CurrencyExistsByIdAsync(short currencyId);
}