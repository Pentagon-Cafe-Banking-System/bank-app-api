using BankApp.Entities;
using BankApp.Models.Requests;

namespace BankApp.Services.AccountService;

public interface IAccountService
{
    Task<IList<Account>> GetAllAccountsAsync(CancellationToken cancellationToken = default);

    Task<bool> IsCustomerAccountOwnerAsync(string userId, long accountId,
        CancellationToken cancellationToken = default);

    Task<Account> GetAccountByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Account> GetAccountByNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<IList<Account>> GetAccountsByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default);
    Task<Account> CreateAccountAsync(CreateAccountRequest request, CancellationToken cancellationToken = default);

    Task<Account> UpdateAccountAsync(UpdateAccountRequest request, long accountId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAccountByIdAsync(long accountId, CancellationToken cancellationToken = default);
    Task<bool> IsAccountActiveByIdAsync(long accountId, CancellationToken cancellationToken = default);
    Task<bool> IsAccountActiveByNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<bool> AccountExistsByIdAsync(long accountId, CancellationToken cancellationToken = default);
    Task<bool> AccountExistsByNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<bool> HasSufficientFundsAsync(long accountId, decimal amount, CancellationToken cancellationToken = default);

    Task<bool> IsWithinTransferLimitAsync(long accountId, decimal amount,
        CancellationToken cancellationToken = default);
}