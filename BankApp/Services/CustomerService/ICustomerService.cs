using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;

namespace BankApp.Services.CustomerService;

public interface ICustomerService
{
    Task<IList<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default);
    Task<Customer> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken = default);
    Task<Customer> CreateCustomerAsync(CreateCustomerRequest request, CancellationToken cancellationToken = default);

    Task<Customer> UpdateCustomerByIdAsync(UpdateCustomerRequest request, string customerId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteCustomerByIdAsync(string customerId);
    Task<bool> CustomerExistsByIdAsync(string customerId, CancellationToken cancellationToken = default);
    Task<bool> NationalIdExistsAsync(string nationalId, CancellationToken cancellationToken = default);
}