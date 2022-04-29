using BankApp.Data;
using BankApp.Entities;

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
        return _dbContext.Currencies.AsEnumerable();
    }
}