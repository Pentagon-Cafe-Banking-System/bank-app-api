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

    public async Task<IList<AccountType>> GetAllAccountTypesAsync()
    {
        var accountTypes = await _dbContext.AccountTypes.ToListAsync();
        return accountTypes;
    }

    public async Task<IList<Currency>> GetCurrenciesOfAccountTypeAsync(short accountTypeId)
    {
        if (accountTypeId == 3)
            return await _dbContext.Currencies.ToListAsync();
        var plnCurrency = await _dbContext.Currencies.SingleAsync(x => x.Code == "PLN");
        return new List<Currency> {plnCurrency};
    }

    public async Task<AccountType> GetAccountTypeByIdAsync(short accountTypeId)
    {
        var accountType = await _dbContext.AccountTypes.FindAsync(accountTypeId);
        if (accountType == null)
            throw new NotFoundException("Account type with requested id does not exist");
        return accountType;
    }
}