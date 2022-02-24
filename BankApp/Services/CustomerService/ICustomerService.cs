using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;

namespace BankApp.Services.CustomerService;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(string id);
    Task CreateCustomerAsync(CreateCustomerRequest request);
    Task DeleteCustomerByIdAsync(string id);
}