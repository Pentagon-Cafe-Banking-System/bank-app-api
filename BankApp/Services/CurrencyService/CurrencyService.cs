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

    public async Task<IList<Currency>> GetAllCurrenciesAsync(CancellationToken cancellationToken = default)
    {
        var currencies = await _dbContext.Currencies.ToListAsync(cancellationToken: cancellationToken);
        return currencies;
    }

    public async Task<Currency> GetCurrencyByIdAsync(short currencyId, CancellationToken cancellationToken = default)
    {
        var currency = await _dbContext.Currencies
            .FindAsync(new object?[] {currencyId}, cancellationToken: cancellationToken);
        if (currency == null)
            throw new NotFoundException("Currency with requested id does not exist");
        return currency;
    }

    public async Task<bool> CurrencyExistsByIdAsync(short currencyId, CancellationToken cancellationToken = default)
    {
        var exists = await _dbContext.Currencies.AnyAsync(c =>
                c.Id == currencyId, cancellationToken: cancellationToken
        );
        return exists;
    }
}