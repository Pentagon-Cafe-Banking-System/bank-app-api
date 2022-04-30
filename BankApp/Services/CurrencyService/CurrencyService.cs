using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.CurrencyService;

public class CurrencyService : ICurrencyService
{
    private readonly ApplicationDbContext _dbContext;

    public CurrencyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Currency>> GetAllCurrencies()
    {
        var currencies = await _dbContext.Currencies.ToListAsync();
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