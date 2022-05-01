using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Models.Responses;
using BankApp.Services.EmployeeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Admin)]
[Route("api/employees")]
[ApiExplorerSettings(GroupName = "Employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    /// <summary>
    /// Returns all employees. Only for admins.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IList<EmployeeDto>>> GetAllEmployeesAsync()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        var employeesDto = employees.Select(e => e.ToDto()).ToList();
        return Ok(employeesDto);
    }

    /// <summary>
    /// Returns employee by id. Only for admins.
    /// </summary>
    [HttpGet("{employeeId}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeByIdAsync(string employeeId)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
        var employeeDto = employee.ToDto();
        return Ok(employeeDto);
    }

    /// <summary>
    /// Creates new employee. Only for admins.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        var employee = await _employeeService.CreateEmployeeAsync(request);
        var employeeDto = employee.ToDto();
        return Ok(employeeDto);
    }

    /// <summary>
    /// Updates employee by id. Only for admins.
    /// </summary>
    [HttpPatch("{employeeId}")] // TODO - make it true PATCH
    public async Task<ActionResult<EmployeeDto>> UpdateEmployeeByIdAsync(UpdateEmployeeRequest request,
        string employeeId)
    {
        var employee = await _employeeService.UpdateEmployeeByIdAsync(request, employeeId);
        var employeeDto = employee.ToDto();
        return Ok(employeeDto);
    }

    /// <summary>
    /// Deletes employee by id. Only for admins.
    /// </summary>
    [HttpDelete("{employeeId}")]
    public async Task<ActionResult<IdentityResult>> DeleteEmployeeByIdAsync(string employeeId)
    {
        var result = await _employeeService.DeleteEmployeeByIdAsync(employeeId);
        return Ok(result);
    }
}