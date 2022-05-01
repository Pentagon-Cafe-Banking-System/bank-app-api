using BankApp.Entities;
using BankApp.Models.Requests;

namespace BankApp.Services.TransferService;

public interface ITransferService
{
    Task<IList<Transfer>> GetAllTransfersAsync(CancellationToken cancellationToken = default);

    Task<IList<Transfer>> GetAllTransfersFromAndToCustomerAsync(string customerId,
        CancellationToken cancellationToken = default);

    Task<Transfer> GetTransferByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Transfer> CreateTransferAsync(CreateTransferRequest request, CancellationToken cancellationToken = default);
}