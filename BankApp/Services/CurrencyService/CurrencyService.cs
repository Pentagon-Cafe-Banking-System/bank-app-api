using BankApp.Data;
using BankApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.CurrencyService;

public class CurrencyService : ICurrencyService
{
    private readonly ApplicationDbContext _dbContext;

    public CurrencyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync()
    {
        return await _dbContext.Currencies.ToListAsync();
    }
}