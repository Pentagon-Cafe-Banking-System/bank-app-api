using BankApp.Entities;
using BankApp.Models.Requests;

namespace BankApp.Services.TransferService;

public interface ITransferService
{
    Task<IList<Transfer>> GetAllTransfersAsync();
    Task<IList<Transfer>> GetAllTransfersFromAndToCustomerAsync(string customerId);
    Task<Transfer> GetTransferByIdAsync(long id);
    Task<Transfer> CreateTransferAsync(CreateTransferRequest request);
}