using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;

namespace BankApp.Services.CustomerService;

public interface ICustomerService
{
    Task<IList<Customer>> GetAllCustomers();
    Task<Customer> GetCustomerByIdAsync(string customerId);
    Task<Customer> CreateCustomerAsync(CreateCustomerRequest request);
    Task<Customer> UpdateCustomerByIdAsync(UpdateCustomerRequest request, string customerId);
    Task<bool> DeleteCustomerByIdAsync(string customerId);
}