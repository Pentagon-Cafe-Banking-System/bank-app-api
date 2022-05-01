using AutoMapper;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions;
using BankApp.Models.Requests;
using BankApp.Services.AccountTypeService;
using BankApp.Services.CurrencyService;
using BankApp.Services.CustomerService;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IAccountTypeService _accountTypeService;
    private readonly ICurrencyService _currencyService;
    private readonly ICustomerService _customerService;
    private readonly ApplicationDbContext _dbContext;

    public AccountService(ApplicationDbContext dbContext, ICustomerService customerService,
        ICurrencyService currencyService, IAccountTypeService accountTypeService)
    {
        _dbContext = dbContext;
        _customerService = customerService;
        _currencyService = currencyService;
        _accountTypeService = accountTypeService;
    }

    public async Task<IList<Account>> GetAllAccountsAsync(CancellationToken cancellationToken = default)
    {
        var accounts = await _dbContext.Accounts.ToListAsync(cancellationToken: cancellationToken);
        return accounts;
    }

    public async Task<bool> IsCustomerAccountOwnerAsync(string userId, long accountId,
        CancellationToken cancellationToken = default)
    {
        var customer = await _customerService.GetCustomerByIdAsync(userId, cancellationToken);
        var result = customer.BankAccounts.Exists(e => e.Id == accountId);
        return result;
    }

    public async Task<Account> GetAccountByIdAsync(long accountId, CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Accounts
            .FindAsync(new object?[] {accountId}, cancellationToken: cancellationToken);
        if (account == null)
            throw new NotFoundException("Account with requested id does not exist");
        return account;
    }

    public Task<Account> GetAccountByNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        var account = _dbContext.Accounts.SingleAsync(a =>
                a.Number == accountNumber, cancellationToken: cancellationToken
        );
        if (account == null)
            throw new NotFoundException("Account with requested number does not exist");
        return account;
    }

    public async Task<IList<Account>> GetAccountsByCustomerIdAsync(string customerId,
        CancellationToken cancellationToken = default)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId, cancellationToken);
        var accounts = customer.BankAccounts.ToList();
        return accounts;
    }

    public async Task<Account> CreateAccountAsync(CreateAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        var accountType = await _accountTypeService.GetAccountTypeByIdAsync(request.AccountTypeId, cancellationToken);
        var currency = await _currencyService.GetCurrencyByIdAsync(request.CurrencyId, cancellationToken);
        var customer = await _customerService.GetCustomerByIdAsync(request.CustomerId, cancellationToken);

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.CreateMap<CreateAccountRequest, Account>()
        ));
        var account = mapper.Map<Account>(request);
        var accountNumber = await GenerateAccountNumber(cancellationToken);
        account.Number = accountNumber;
        account.AccountType = accountType;
        account.Currency = currency;
        account.IsActive = request.IsActive;
        customer.BankAccounts.Add(account);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return account;
    }

    public async Task<Account> UpdateAccountAsync(UpdateAccountRequest request, long accountId,
        CancellationToken cancellationToken = default)
    {
        var account = await GetAccountByIdAsync(accountId, cancellationToken);
        account.Balance = request.Balance ?? account.Balance;
        account.TransferLimit = request.TransferLimit ?? account.TransferLimit;
        account.IsActive = request.IsActive ?? account.IsActive;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return account;
    }

    public async Task<bool> DeleteAccountByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var account = await GetAccountByIdAsync(id, cancellationToken);
        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> IsAccountActiveByIdAsync(long accountId, CancellationToken cancellationToken = default)
    {
        var account = await GetAccountByIdAsync(accountId, cancellationToken);
        return account.IsActive;
    }

    public async Task<bool> IsAccountActiveByNumberAsync(string accountNumber,
        CancellationToken cancellationToken = default)
    {
        var account = await GetAccountByNumberAsync(accountNumber, cancellationToken);
        return account.IsActive;
    }

    public Task<bool> AccountExistsByIdAsync(long accountId, CancellationToken cancellationToken = default)
    {
        var exists = _dbContext.Accounts.AnyAsync(a =>
                a.Id == accountId, cancellationToken: cancellationToken
        );
        return exists;
    }

    public async Task<bool> AccountExistsByNumberAsync(string accountNumber,
        CancellationToken cancellationToken = default)
    {
        var exists = await _dbContext.Accounts.AnyAsync(a =>
                a.Number == accountNumber, cancellationToken: cancellationToken
        );
        return exists;
    }

    public async Task<bool> HasSufficientFundsAsync(long accountId, decimal amount,
        CancellationToken cancellationToken = default)
    {
        var account = await GetAccountByIdAsync(accountId, cancellationToken);
        return account.Balance >= amount;
    }

    public async Task<bool> IsWithinTransferLimitAsync(long accountId, decimal amount,
        CancellationToken cancellationToken = default)
    {
        var account = await GetAccountByIdAsync(accountId, cancellationToken);
        return account.TransferLimit >= amount;
    }

    private async Task<string> GenerateAccountNumber(CancellationToken cancellationToken = default)
    {
        long id;
        try
        {
            id = await _dbContext.Accounts.MaxAsync(acc => acc.Id, cancellationToken: cancellationToken) + 1;
        }
        catch (Exception)
        {
            id = 1;
        }

        var accountNumber = id.ToString().PadLeft(16, '0');
        return accountNumber;
    }
}