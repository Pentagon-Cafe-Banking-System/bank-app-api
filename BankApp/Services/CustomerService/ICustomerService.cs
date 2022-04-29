using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.CustomerService;

public interface ICustomerService
{
    IEnumerable<Customer> GetAllCustomers();
    Task<Customer> GetCustomerByIdAsync(string id);
    Task<Customer> CreateCustomerAsync(CreateCustomerRequest request);
    Task<Customer> UpdateCustomerByIdAsync(UpdateCustomerRequest request, string id);
    Task<IdentityResult> DeleteCustomerByIdAsync(string id);
}