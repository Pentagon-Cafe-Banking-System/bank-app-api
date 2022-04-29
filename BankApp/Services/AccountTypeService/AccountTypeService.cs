using BankApp.Data;
using BankApp.Entities;

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

    public IEnumerable<Currency> GetCurrenciesByAccountTypeId(short accountTypeId)
    {
        if (accountTypeId == 3)
            return _dbContext.Currencies.AsEnumerable();
        return new List<Currency> {_dbContext.Currencies.Single(x => x.Code == "PLN")}.AsEnumerable();
    }
}