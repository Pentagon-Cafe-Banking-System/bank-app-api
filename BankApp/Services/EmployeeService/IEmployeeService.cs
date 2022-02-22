using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;

namespace BankApp.Services.EmployeeService;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(Int64 id);
    Task CreateEmployeeAsync(CreateEmployeeRequest request);
    Task DeleteEmployeeByIdAsync(Int64 id);
}