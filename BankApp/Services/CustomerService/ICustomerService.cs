using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.CustomerService;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(string id);
    Task<Customer> CreateCustomerAsync(CreateCustomerRequest request);
    Task<IdentityResult> DeleteCustomerByIdAsync(string id);
}