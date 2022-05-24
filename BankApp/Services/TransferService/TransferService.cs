using AutoMapper;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions;
using BankApp.Models.Requests;
using BankApp.Services.AccountService;
using BankApp.Services.CustomerService;
using Castle.Core.Internal;
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

    public async Task<IList<Transfer>> GetAllTransfersAsync(CancellationToken cancellationToken = default)
    {
        var transfers = await _dbContext.Transfers.ToListAsync(cancellationToken: cancellationToken);
        return transfers;
    }

    public async Task<IEnumerable<Transfer>> GetAllTransfersFromAndToCustomerAsync(string customerId,
        CancellationToken cancellationToken = default)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId, cancellationToken);
        var customerAccounts = customer.BankAccounts;
        var accountIds = customerAccounts.Select(a => a.Id);
        var accountNumbers = customerAccounts.Select(a => a.Number);
        var transfers = _dbContext.Transfers.Where(t =>
            accountNumbers.Contains(t.ReceiverAccountNumber) || accountIds.Contains(t.SenderAccountId)
        );
        return transfers;
    }

    public async Task<IEnumerable<Transfer>> GetFilteredTransfersFromAndToCustomerAsync(string customerId,
        decimal? lowestAmount, decimal? highestAmount, string? title, int? takeFirst,
        CancellationToken cancellationToken = default)
    {
        var transfers = await GetAllTransfersFromAndToCustomerAsync(customerId, cancellationToken);

        if (lowestAmount.HasValue)
            transfers = transfers.Where(t => t.Amount >= lowestAmount);
        if (highestAmount.HasValue)
            transfers = transfers.Where(t => t.Amount <= highestAmount);
        if (!title.IsNullOrEmpty())
            transfers = transfers.Where(t => t.Title.ToUpper().Contains(title!.ToUpper()));
        if (takeFirst.HasValue)
            transfers = transfers.Take(takeFirst.Value);

        var transfersFilteredAsList = transfers.ToList();
        if (transfersFilteredAsList.IsNullOrEmpty())
            throw new NotFoundException("No transfers match the search criteria");
        return transfersFilteredAsList;
    }

    public async Task<Transfer> GetTransferByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var transfer = await _dbContext.Transfers.FindAsync(new object?[] {id}, cancellationToken: cancellationToken);
        if (transfer == null)
            throw new NotFoundException("Transfer with requested id does not exist");
        return transfer;
    }

    public async Task<Transfer> CreateTransferAsync(CreateTransferRequest request,
        CancellationToken cancellationToken = default)
    {
        var senderAccount = await _accountService.GetAccountByIdAsync(request.SenderAccountId, cancellationToken);
        var receiverAccount =
            await _accountService.GetAccountByNumberAsync(request.ReceiverAccountNumber, cancellationToken);
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

        await _dbContext.SaveChangesAsync(cancellationToken);
        return transfer;
    }
}