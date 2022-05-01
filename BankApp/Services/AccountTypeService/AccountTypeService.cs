using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.AccountTypeService;

public class AccountTypeService : IAccountTypeService
{
    private readonly ApplicationDbContext _dbContext;

    public AccountTypeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<AccountType>> GetAllAccountTypesAsync(CancellationToken cancellationToken = default)
    {
        var accountTypes = await _dbContext.AccountTypes.ToListAsync(cancellationToken: cancellationToken);
        return accountTypes;
    }

    public async Task<IList<Currency>> GetCurrenciesOfAccountTypeAsync(int accountTypeId,
        CancellationToken cancellationToken = default)
    {
        var accountType = await GetAccountTypeByIdAsync(accountTypeId, cancellationToken);
        var currencies = accountType.AvailableCurrencies
            .Select(ac => ac.Currency)
            .ToList();
        return currencies;
    }

    public async Task<AccountType> GetAccountTypeByIdAsync(int accountTypeId,
        CancellationToken cancellationToken = default)
    {
        var accountType = await _dbContext.AccountTypes
            .FindAsync(new object?[] {accountTypeId}, cancellationToken: cancellationToken);
        if (accountType == null)
            throw new NotFoundException("Account type with requested id does not exist");
        return accountType;
    }

    public async Task<bool> AccountTypeExistsByIdAsync(int accountTypeId,
        CancellationToken cancellationToken = default)
    {
        var exists = await _dbContext.AccountTypes.AnyAsync(x =>
                x.Id == accountTypeId, cancellationToken: cancellationToken
        );
        return exists;
    }

    public async Task<bool> AccountTypeSupportsCurrencyAsync(int accountTypeId, int currencyId,
        CancellationToken cancellationToken = default)
    {
        var accountType = await GetAccountTypeByIdAsync(accountTypeId, cancellationToken);
        var supportsCurrency = accountType.AvailableCurrencies.Any(x => x.CurrencyId == currencyId);
        return supportsCurrency;
    }
}