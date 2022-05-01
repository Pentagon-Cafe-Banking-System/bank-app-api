using BankApp.Entities;

namespace BankApp.Services.AccountTypeService;

public interface IAccountTypeService
{
    Task<IList<AccountType>> GetAllAccountTypesAsync(CancellationToken cancellationToken = default);

    Task<IList<Currency>> GetCurrenciesOfAccountTypeAsync(short accountTypeId,
        CancellationToken cancellationToken = default);

    Task<AccountType> GetAccountTypeByIdAsync(short accountTypeId, CancellationToken cancellationToken = default);
    Task<bool> AccountTypeExistsByIdAsync(short accountTypeId, CancellationToken cancellationToken = default);
}