using BankApp.Data;
using BankApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.AccountTypeService;

public class AccountTypeService : IAccountTypeService
{
    private readonly ApplicationDbContext _dbContext;

    public AccountTypeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AccountType>> GetAllAccountTypesAsync()
    {
        return await _dbContext.AccountTypes.ToListAsync();
    }

    public async Task<IEnumerable<Currency>> GetCurrenciesByAccountTypeIdAsync(short accountTypeId)
    {
        if (accountTypeId == 3)
            return await _dbContext.Currencies.ToListAsync();
        return await _dbContext.Currencies.Where(x => x.Code == "PLN").ToListAsync();
    }
}