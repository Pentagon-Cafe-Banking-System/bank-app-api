using BankApp.Entities;

namespace BankApp.Services.CurrencyService;

public interface ICurrencyService
{
    IEnumerable<Currency> GetAllCurrencies();
    Task<Currency> GetCurrencyByIdAsync(short id);
}