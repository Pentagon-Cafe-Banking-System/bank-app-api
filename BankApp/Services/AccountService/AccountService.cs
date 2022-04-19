using AutoMapper;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions;
using BankApp.Exceptions.RequestExceptions;
using BankApp.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _dbContext;
    
    public AccountService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Account>> GetAllAccountsAsync()
    {
        var accounts = await _dbContext.Accounts.ToListAsync();
        return accounts;
    }

    public async Task<Account> GetAccountByIdAsync(long id)
    {
        var account = await _dbContext.Accounts.FindAsync(id);
        if (account == null)
            throw new NotFoundException("Id", "Account with requested id could not be found");
        return account;
    }

    public async Task<Account> CreateAccountAsync(CreateAccountRequest request)
    {
        await using (var dbContextTransaction = await _dbContext.Database.BeginTransactionAsync())
        {
            var accountTypeId = request.AccountTypeId;
            var currencyId = request.CurrencyId;
            var customerId = request.CustomerId;
            var accountType = _dbContext.AccountTypes.Find(accountTypeId);
            var currency = _dbContext.Currencies.Find(currencyId);
            var customer = _dbContext.Customers.Find(customerId);
            if (accountType == null)
                throw new AppException("Account type with requested id could not be found");
            if (currency == null)
                throw new AppException("Currency with requested id could not be found");
            if (customer == null)
                throw new AppException("Customer with requested id could not be found");
            var mapper = new Mapper(
                new MapperConfiguration(cfg =>
                    cfg.CreateMap<CreateAccountRequest, Account>()
                )
            );
            var account = mapper.Map<Account>(request);
            account.AccountType = accountType;
            account.Currency = currency;
            var idAccount = _dbContext.Accounts.Select(acc => acc.Id).Max() + 1;
            var id = idAccount.ToString();
            var numberOfZeros = 16 - id.Length;
            var paddedId = id.PadLeft(numberOfZeros, '0');
            account.Number = paddedId;
            account.IsActive = true;
            customer.BankAccounts.Add(account);

            await _dbContext.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();

            return account;
        }
    }

    public async Task<Account> UpdateAccountAsync(UpdateAccountRequest request, long id)
    {
        var account = await GetAccountByIdAsync(id);
        account.Balance = request.Balance;
        account.TransferLimit = request.TransferLimit;
        account.IsActive = request.IsActive;
        await _dbContext.SaveChangesAsync();
        return account;
    }
}