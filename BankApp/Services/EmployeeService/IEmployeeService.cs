using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;

namespace BankApp.Services.EmployeeService;

public interface IEmployeeService
{
    Task<IList<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);
    Task<Employee> GetEmployeeByIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task<Employee> CreateEmployeeAsync(CreateEmployeeRequest request, CancellationToken cancellationToken = default);

    Task<Employee> UpdateEmployeeByIdAsync(UpdateEmployeeRequest request, string employeeId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteEmployeeByIdAsync(string employeeId);
}