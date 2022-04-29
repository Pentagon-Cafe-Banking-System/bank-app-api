using BankApp.Entities;
using BankApp.Models.Requests;

namespace BankApp.Services.TransferService;

public interface ITransferService
{
    IEnumerable<Transfer> GetAllTransfers();
    Task<IEnumerable<Transfer>> GetAllTransfersFromAndToCustomerByIdAsync(string customerId);
    Task<Transfer> GetTransferByIdAsync(long id);
    Task<Transfer> CreateTransferAsync(CreateTransferRequest request);
}