using BankApp.Entities;

namespace BankApp.Services.AccountTypeService;

public interface IAccountTypeService
{
    Task<IEnumerable<AccountType>> GetAllAccountTypesAsync();
    Task<IEnumerable<Currency>> GetCurrenciesByAccountTypeIdAsync(short accountTypeId);
}