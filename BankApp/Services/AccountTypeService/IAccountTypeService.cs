using BankApp.Entities;

namespace BankApp.Services.AccountTypeService;

public interface IAccountTypeService
{
    Task<IList<AccountType>> GetAllAccountTypesAsync(CancellationToken cancellationToken = default);

    Task<IList<Currency>> GetCurrenciesOfAccountTypeAsync(int accountTypeId,
        CancellationToken cancellationToken = default);

    Task<AccountType> GetAccountTypeByIdAsync(int accountTypeId, CancellationToken cancellationToken = default);
    Task<bool> AccountTypeExistsByIdAsync(int accountTypeId, CancellationToken cancellationToken = default);

    Task<bool> AccountTypeSupportsCurrencyAsync(int accountTypeId, int currencyId,
        CancellationToken cancellationToken = default);
}