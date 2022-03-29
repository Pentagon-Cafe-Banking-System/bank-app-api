using BankApp.Entities;

namespace BankApp.Services.AccountService;

public interface IAccountTypeService
{
    Task<IEnumerable<AccountType>> GetAllAccountTypesAsync();
}