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

    public IEnumerable<AccountType> GetAllAccountTypes()
    {
        var accountTypes = _dbContext.AccountTypes.AsEnumerable();
        return accountTypes;
    }

    public async Task<IEnumerable<Currency>> GetCurrenciesByAccountTypeId(short accountTypeId)
    {
        if (accountTypeId == 3)
            return _dbContext.Currencies.AsEnumerable();
        var plnCurrency = await _dbContext.Currencies.SingleAsync(x => x.Code == "PLN");
        return new List<Currency> {plnCurrency}.AsEnumerable();
    }

    public async Task<AccountType> GetAccountTypeByIdAsync(short accountTypeId)
    {
        var accountType = await _dbContext.AccountTypes.FindAsync(accountTypeId);
        if (accountType == null)
            throw new AppException("Account type with requested id does not exist");
        return accountType;
    }
}