using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;

namespace BankApp.Services.CustomerService;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(Int64 id);
    Task CreateCustomerAsync(CreateCustomerRequest request);
    Task DeleteCustomerByIdAsync(Int64 id);
}