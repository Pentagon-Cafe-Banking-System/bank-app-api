using BankApp.Entities;

namespace BankApp.Services.AccountTypeService;

public interface IAccountTypeService
{
    IEnumerable<AccountType> GetAllAccountTypes();
    Task<IEnumerable<Currency>> GetCurrenciesByAccountTypeId(short accountTypeId);
    Task<AccountType> GetAccountTypeByIdAsync(short accountTypeId);
}