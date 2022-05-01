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

    public async Task<IList<Account>> GetAllAccountsAsync()
    {
        var accounts = await _dbContext.Accounts.ToListAsync();
        return accounts;
    }

    public async Task<bool> IsCustomerAccountOwnerAsync(string userId, long accountId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(userId);
        var result = customer.BankAccounts.Exists(e => e.Id == accountId);
        return result;
    }

    public async Task<Account> GetAccountByIdAsync(long accountId)
    {
        var account = await _dbContext.Accounts.FindAsync(accountId);
        if (account == null)
            throw new NotFoundException("Account with requested id does not exist");
        return account;
    }

    public Task<Account> GetAccountByNumberAsync(string accountNumber)
    {
        var account = _dbContext.Accounts.SingleAsync(a => a.Number == accountNumber);
        if (account == null)
            throw new NotFoundException("Account with requested number does not exist");
        return account;
    }

    public async Task<IList<Account>> GetAccountsByCustomerIdAsync(string customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        var accounts = customer.BankAccounts.ToList();
        return accounts;
    }

    public async Task<Account> CreateAccountAsync(CreateAccountRequest request)
    {
        var accountType = await _accountTypeService.GetAccountTypeByIdAsync(request.AccountTypeId);
        var currency = await _currencyService.GetCurrencyByIdAsync(request.CurrencyId);
        var customer = await _customerService.GetCustomerByIdAsync(request.CustomerId);

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.CreateMap<CreateAccountRequest, Account>()
        ));
        var account = mapper.Map<Account>(request);
        var accountNumber = GenerateAccountNumber();
        account.Number = accountNumber;
        account.AccountType = accountType;
        account.Currency = currency;
        account.IsActive = request.IsActive;
        customer.BankAccounts.Add(account);

        await _dbContext.SaveChangesAsync();
        return account;
    }

    public async Task<Account> UpdateAccountAsync(UpdateAccountRequest request, long accountId)
    {
        var account = await GetAccountByIdAsync(accountId);
        account.Balance = request.Balance ?? account.Balance;
        account.TransferLimit = request.TransferLimit ?? account.TransferLimit;
        account.IsActive = request.IsActive ?? account.IsActive;
        await _dbContext.SaveChangesAsync();
        return account;
    }

    public async Task<bool> DeleteAccountByIdAsync(long id)
    {
        var account = await GetAccountByIdAsync(id);
        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsAccountActiveByIdAsync(long accountId)
    {
        var account = await GetAccountByIdAsync(accountId);
        return account.IsActive;
    }

    public async Task<bool> IsAccountActiveByNumberAsync(string accountNumber)
    {
        var account = await GetAccountByNumberAsync(accountNumber);
        return account.IsActive;
    }

    public Task<bool> AccountExistsByIdAsync(long accountId)
    {
        var exists = _dbContext.Accounts.AnyAsync(a => a.Id == accountId);
        return exists;
    }

    public async Task<bool> AccountExistsByNumberAsync(string accountNumber)
    {
        var exists = await _dbContext.Accounts.AnyAsync(a => a.Number == accountNumber);
        return exists;
    }

    public async Task<bool> HasSufficientFundsAsync(long accountId, decimal amount)
    {
        var account = await GetAccountByIdAsync(accountId);
        return account.Balance >= amount;
    }

    public async Task<bool> IsWithinTransferLimitAsync(long accountId, decimal amount)
    {
        var account = await GetAccountByIdAsync(accountId);
        return account.TransferLimit >= amount;
    }

    private string GenerateAccountNumber()
    {
        long id;
        try
        {
            id = _dbContext.Accounts.Max(acc => acc.Id) + 1;
        }
        catch (Exception)
        {
            id = 1;
        }

        var accountNumber = id.ToString().PadLeft(16, '0');
        return accountNumber;
    }
}