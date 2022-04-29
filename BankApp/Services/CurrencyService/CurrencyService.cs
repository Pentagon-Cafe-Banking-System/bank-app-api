using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions;

namespace BankApp.Services.CurrencyService;

public class CurrencyService : ICurrencyService
{
    private readonly ApplicationDbContext _dbContext;

    public CurrencyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Currency> GetAllCurrencies()
    {
        var currencies = _dbContext.Currencies.AsEnumerable();
        return currencies;
    }

    public async Task<Currency> GetCurrencyByIdAsync(short id)
    {
        var currency = await _dbContext.Currencies.FindAsync(id);
        if (currency == null)
            throw new AppException("Currency with requested id does not exist");
        return currency;
    }
}