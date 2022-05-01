using BankApp.Entities;

namespace BankApp.Services.AccountTypeService;

public interface IAccountTypeService
{
    Task<IList<AccountType>> GetAllAccountTypesAsync();
    Task<IList<Currency>> GetCurrenciesOfAccountTypeAsync(short accountTypeId);
    Task<AccountType> GetAccountTypeByIdAsync(short accountTypeId);
}