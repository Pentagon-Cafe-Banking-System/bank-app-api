using AutoMapper;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions;
using BankApp.Exceptions.RequestExceptions;
using BankApp.Models.Requests;
using BankApp.Services.CustomerService;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.TransferService;

public class TransferService : ITransferService
{
    private readonly ICustomerService _customerService;
    private readonly ApplicationDbContext _dbContext;

    public TransferService(ApplicationDbContext dbContext, ICustomerService customerService)
    {
        _dbContext = dbContext;
        _customerService = customerService;
    }

    public async Task<IEnumerable<Transfer>> GetAllTransfersAsync()
    {
        var transfers = await _dbContext.Transfers.ToListAsync();
        return transfers;
    }

    public async Task<IEnumerable<Transfer>> GetAllTransfersFromAndToCustomerAsync(string customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        var accountIds = customer.BankAccounts.Select(a => a.Id);
        var accountNumbers = customer.BankAccounts.Select(a => a.Number);
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
            throw new NotFoundException("Id", "Transfer with requested id could not be found");
        return transfer;
    }

    public async Task<Transfer> CreateTransferAsync(CreateTransferRequest request)
    {
        await using (var dbContextTransaction = await _dbContext.Database.BeginTransactionAsync())
        {
            var ordered = DateTime.UtcNow;
            var senderAccountId = request.SenderAccountId;
            var account = await _dbContext.Accounts.FindAsync(senderAccountId);
            if (account == null)
                throw new AppException("Account with requested id could not be found");
            var receiverNumber = request.ReceiverAccountNumber;
            var receiver = _dbContext.Accounts.Single(e => e.Number == receiverNumber);
            var mapper = new Mapper(
                new MapperConfiguration(cfg =>
                    cfg.CreateMap<CreateTransferRequest, Transfer>()
                )
            );
            var receiverCurrency = _dbContext.Accounts.Single(e => e.Number == receiverNumber).Currency;
            var senderCurrency = _dbContext.Accounts.Single(e => e.Id == senderAccountId).Currency;
            if (receiverCurrency != senderCurrency)
            {
                account.Balance -= request.Amount;
                receiver.Balance += request.Amount * senderCurrency.Bid / receiverCurrency.Bid;
            }
            else
            {
                account.Balance -= request.Amount;
                receiver.Balance += request.Amount;
            }

            var transfer = mapper.Map<Transfer>(request);
            transfer.Ordered = ordered;
            transfer.Executed = DateTime.UtcNow;
            transfer.IsCompleted = true;
            account.Transfers.Add(transfer);
            await _dbContext.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();

            return transfer;
        }
    }
}