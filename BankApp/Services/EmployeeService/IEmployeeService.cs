using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;

namespace BankApp.Services.EmployeeService;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(string id);
    Task CreateEmployeeAsync(CreateEmployeeRequest request);
    Task DeleteEmployeeByIdAsync(string id);
}