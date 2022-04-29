using BankApp.Entities;
using BankApp.Models.Requests;

namespace BankApp.Services.AccountService;

public interface IAccountService
{
    IEnumerable<Account> GetAllAccounts();
    Task<bool> IsCustomerAccountOwnerAsync(string userId, long accountId);
    Task<Account> GetAccountByIdAsync(long id);
    Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(string customerId);
    Task<Account> CreateAccountAsync(CreateAccountRequest request);
    Task<Account> UpdateAccountAsync(UpdateAccountRequest request, long id);
    Task<bool> DeleteAccountByIdAsync(long id);
}