using AutoMapper;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Exceptions;
using BankApp.Exceptions.RequestExceptions;
using BankApp.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.TransferService;

public class TransferService : ITransferService
{
    private readonly ApplicationDbContext _dbContext;
    
    public TransferService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Transfer>> GetAllTransfersAsync()
    {
        var transfers = await _dbContext.Transfers.ToListAsync();
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
            var accountId = request.AccountId;
            var account = _dbContext.Accounts.Find(accountId);
            if (account == null)
                throw new AppException("Account with requested id could not be found");
            var receiverNumber = request.ReceiverAccountNumber;
            var receiver = _dbContext.Accounts.Single(e => e.Number == receiverNumber);
            var mapper = new Mapper(
                new MapperConfiguration(cfg =>
                    cfg.CreateMap<CreateTransferRequest, Transfer>()
                )
            );
            var receiverCurrency = _dbContext.Accounts.Single(e=>e.Number == receiverNumber).Currency;
            var senderCurrency = _dbContext.Accounts.Single(e=>e.Id == accountId).Currency;
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