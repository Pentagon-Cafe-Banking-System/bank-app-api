using BankApp.Data;
using BankApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.AccountService;

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
}