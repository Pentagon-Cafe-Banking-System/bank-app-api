using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.EmployeeService;

public interface IEmployeeService
{
    IEnumerable<Employee> GetAllEmployees();
    Task<Employee> GetEmployeeByIdAsync(string id);
    Task<Employee> CreateEmployeeAsync(CreateEmployeeRequest request);
    Task<Employee> UpdateEmployeeByIdAsync(UpdateEmployeeRequest request, string id);
    Task<IdentityResult> DeleteEmployeeByIdAsync(string id);
}