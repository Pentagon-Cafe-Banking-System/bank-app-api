using BankApp.Entities;

namespace BankApp.Services.CurrencyService;

public interface ICurrencyService
{
    Task<IEnumerable<Currency>> GetAllCurrencies();
    Task<Currency> GetCurrencyByIdAsync(short id);
}