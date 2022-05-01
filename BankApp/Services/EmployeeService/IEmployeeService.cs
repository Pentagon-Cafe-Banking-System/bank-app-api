using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;

namespace BankApp.Services.EmployeeService;

public interface IEmployeeService
{
    Task<IList<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(string employeeId);
    Task<Employee> CreateEmployeeAsync(CreateEmployeeRequest request);
    Task<Employee> UpdateEmployeeByIdAsync(UpdateEmployeeRequest request, string employeeId);
    Task<bool> DeleteEmployeeByIdAsync(string employeeId);
}