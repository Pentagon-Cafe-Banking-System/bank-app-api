using BankApp.Entities;
using BankApp.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.AccountService;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccountsAsync();
    Task<bool> IsUserAccountOwnerAsync(string userId, long accountId);
    Task<Account> GetAccountByIdAsync(long id);
    Task<Account> CreateAccountAsync(CreateAccountRequest request);
    Task<Account> UpdateAccountAsync(UpdateAccountRequest request, long id);
}