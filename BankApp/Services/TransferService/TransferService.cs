using AutoMapper;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions;
using BankApp.Models.Requests;
using BankApp.Services.AccountService;
using BankApp.Services.CustomerService;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.TransferService;

public class TransferService : ITransferService
{
    private readonly IAccountService _accountService;
    private readonly ICustomerService _customerService;
    private readonly ApplicationDbContext _dbContext;

    public TransferService(ApplicationDbContext dbContext, ICustomerService customerService,
        IAccountService accountService)
    {
        _dbContext = dbContext;
        _customerService = customerService;
        _accountService = accountService;
    }

    public async Task<IList<Transfer>> GetAllTransfersAsync()
    {
        var transfers = await _dbContext.Transfers.ToListAsync();
        return transfers;
    }

    public async Task<IList<Transfer>> GetAllTransfersFromAndToCustomerAsync(string customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        var customerBankAccounts = customer.BankAccounts;
        var accountIds = customerBankAccounts.Select(a => a.Id);
        var accountNumbers = customerBankAccounts.Select(a => a.Number);
        var transfers = await _dbContext.Transfers
            .Where(t => accountNumbers.Contains(t.ReceiverAccountNumber) || accountIds.Contains(t.SenderAccountId))
            .OrderByDescending(t => t.Ordered)
            .ToListAsync();
        return transfers;
    }

    public async Task<Transfer> GetTransferByIdAsync(long id)
    {
        var transfer = await _dbContext.Transfers.FindAsync(id);
        if (transfer == null)
            throw new NotFoundException("Transfer with requested id does not exist");
        return transfer;
    }

    public async Task<Transfer> CreateTransferAsync(CreateTransferRequest request)
    {
        var senderAccount = await _accountService.GetAccountByIdAsync(request.SenderAccountId);
        var receiverAccount = await _accountService.GetAccountByNumberAsync(request.ReceiverAccountNumber);
        var receiverCurrency = receiverAccount.Currency;
        var senderCurrency = senderAccount.Currency;

        if (receiverCurrency != senderCurrency)
        {
            senderAccount.Balance -= request.Amount;
            receiverAccount.Balance += request.Amount * senderCurrency.Bid / receiverCurrency.Bid;
        }
        else
        {
            senderAccount.Balance -= request.Amount;
            receiverAccount.Balance += request.Amount;
        }

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.CreateMap<CreateTransferRequest, Transfer>()
        ));
        var transfer = mapper.Map<Transfer>(request);
        transfer.Ordered = DateTime.UtcNow;
        transfer.Executed = DateTime.UtcNow;
        transfer.IsCompleted = true;
        senderAccount.Transfers.Add(transfer);

        await _dbContext.SaveChangesAsync();
        return transfer;
    }
}