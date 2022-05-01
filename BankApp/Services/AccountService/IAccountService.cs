using BankApp.Entities;
using BankApp.Models.Requests;

namespace BankApp.Services.AccountService;

public interface IAccountService
{
    Task<IList<Account>> GetAllAccountsAsync();
    Task<bool> IsCustomerAccountOwnerAsync(string userId, long accountId);
    Task<Account> GetAccountByIdAsync(long id);
    Task<Account> GetAccountByNumberAsync(string accountNumber);
    Task<IList<Account>> GetAccountsByCustomerIdAsync(string customerId);
    Task<Account> CreateAccountAsync(CreateAccountRequest request);
    Task<Account> UpdateAccountAsync(UpdateAccountRequest request, long accountId);
    Task<bool> DeleteAccountByIdAsync(long accountId);
    Task<bool> IsAccountActiveByIdAsync(long accountId);
    Task<bool> IsAccountActiveByNumberAsync(string accountNumber);
    Task<bool> AccountExistsByIdAsync(long accountId);
    Task<bool> AccountExistsByNumberAsync(string accountNumber);
    Task<bool> HasSufficientFundsAsync(long accountId, decimal amount);
    Task<bool> IsWithinTransferLimitAsync(long accountId, decimal amount);
}