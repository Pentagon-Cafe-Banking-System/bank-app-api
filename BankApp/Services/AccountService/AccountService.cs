using AutoMapper;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions.RequestExceptions;
using BankApp.Models;
using BankApp.Models.Requests;
using Microsoft.AspNetCore.Identity;
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
            var mapper = new Mapper(
                new MapperConfiguration(cfg =>
                    cfg.CreateMap<CreateAccountRequest, Account>()
                )
            );
            var account = mapper.Map<Account>(request);
            account.AccountType = accountType;
            account.Currency = currency;
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