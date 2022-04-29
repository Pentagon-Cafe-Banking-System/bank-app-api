using BankApp.Entities;

namespace BankApp.Services.AccountTypeService;

public interface IAccountTypeService
{
    IEnumerable<AccountType> GetAllAccountTypes();
    IEnumerable<Currency> GetCurrenciesByAccountTypeId(short accountTypeId);
}