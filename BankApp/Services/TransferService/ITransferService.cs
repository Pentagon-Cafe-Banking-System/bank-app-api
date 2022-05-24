using BankApp.Entities;
using BankApp.Models.Requests;

namespace BankApp.Services.TransferService;

public interface ITransferService
{
    Task<IList<Transfer>> GetAllTransfersAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<Transfer>> GetAllTransfersFromAndToCustomerAsync(string customerId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Transfer>> GetFilteredTransfersFromAndToCustomerAsync(string customerId, decimal? lowestAmount,
        decimal? highestAmount, string? title, int? takeFirst, CancellationToken cancellationToken = default);

    Task<Transfer> GetTransferByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Transfer> CreateTransferAsync(CreateTransferRequest request, CancellationToken cancellationToken = default);
}