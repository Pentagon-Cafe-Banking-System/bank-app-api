using AutoMapper;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions.RequestErrors;
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

    public IEnumerable<Account> GetAllAccounts()
    {
        var accounts = _dbContext.Accounts.AsEnumerable();
        return accounts;
    }

    public async Task<bool> IsCustomerAccountOwnerAsync(string userId, long accountId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(userId);
        var result = customer.BankAccounts.Exists(e => e.Id == accountId);
        return result;
    }

    public async Task<Account> GetAccountByIdAsync(long id)
    {
        var account = await _dbContext.Accounts.FindAsync(id);
        if (account == null)
            throw new NotFoundError("Id", "Account with requested id could not be found");
        return account;
    }

    public Task<Account> GetAccountByNumberAsync(string number)
    {
        var account = _dbContext.Accounts.SingleAsync(a => a.Number == number);
        if (account == null)
            throw new NotFoundError("Number", "Account with requested number could not be found");
        return account;
    }

    public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(string customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        var accounts = customer.BankAccounts.AsEnumerable();
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

    public async Task<Account> UpdateAccountAsync(UpdateAccountRequest request, long id)
    {
        var account = await GetAccountByIdAsync(id);
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

    private string GenerateAccountNumber()
    {
        var id = (_dbContext.Accounts.MaxBy(acc => acc.Id)?.Id ?? 0) + 1;
        var accountNumber = id.ToString().PadLeft(16, '0');
        return accountNumber;
    }
}