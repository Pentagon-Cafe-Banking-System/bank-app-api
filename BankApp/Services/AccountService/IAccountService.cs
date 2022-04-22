using BankApp.Entities;
using BankApp.Models.Requests;

namespace BankApp.Services.AccountService;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccountsAsync();
    Task<bool> IsCustomerAccountOwnerAsync(string userId, long accountId);
    Task<Account> GetAccountByIdAsync(long id);
    Task<Account> CreateAccountAsync(CreateAccountRequest request);
    Task<Account> UpdateAccountAsync(UpdateAccountRequest request, long id);
    Task<bool> DeleteAccountAsync(long id);
}