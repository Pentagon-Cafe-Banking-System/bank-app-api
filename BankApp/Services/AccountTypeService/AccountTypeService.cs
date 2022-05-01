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

    public async Task<IList<Currency>> GetCurrenciesOfAccountTypeAsync(short accountTypeId,
        CancellationToken cancellationToken = default)
    {
        if (accountTypeId == 3)
            return await _dbContext.Currencies.ToListAsync(cancellationToken: cancellationToken);
        var plnCurrency = await _dbContext.Currencies.SingleAsync(x =>
                x.Code == "PLN", cancellationToken: cancellationToken
        );
        return new List<Currency> {plnCurrency};
    }

    public async Task<AccountType> GetAccountTypeByIdAsync(short accountTypeId,
        CancellationToken cancellationToken = default)
    {
        var accountType = await _dbContext.AccountTypes
            .FindAsync(new object?[] {accountTypeId}, cancellationToken: cancellationToken);
        if (accountType == null)
            throw new NotFoundException("Account type with requested id does not exist");
        return accountType;
    }

    public async Task<bool> AccountTypeExistsByIdAsync(short accountTypeId,
        CancellationToken cancellationToken = default)
    {
        var exists = await _dbContext.AccountTypes.AnyAsync(x =>
                x.Id == accountTypeId, cancellationToken: cancellationToken
        );
        return exists;
    }
}