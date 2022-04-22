using BankApp.Entities;
using BankApp.Models.Requests;

namespace BankApp.Services.TransferService;

public interface ITransferService
{
    Task<IEnumerable<Transfer>> GetAllTransfersAsync();
    Task<IEnumerable<Transfer>> GetAllTransfersFromAndToCustomerAsync(string customerId);
    Task<Transfer> GetTransferByIdAsync(long id);
    Task<Transfer> CreateTransferAsync(CreateTransferRequest request);
}