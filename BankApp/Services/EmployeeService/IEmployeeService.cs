using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.EmployeeService;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(string id);
    Task<Employee> CreateEmployeeAsync(CreateEmployeeRequest request);
    Task<Employee> UpdateEmployeeAsync(UpdateEmployeeRequest request, string id);
    Task<IdentityResult> DeleteEmployeeByIdAsync(string id);
}