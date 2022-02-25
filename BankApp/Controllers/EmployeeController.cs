using BankApp.Entities.UserTypes;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Services.EmployeeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Admin)]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(string id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        return Ok(employee);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeRequest request)
    {
        var employee = await _employeeService.CreateEmployeeAsync(request);
        return Ok(employee);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<IdentityResult>> DeleteEmployeeById(string id)
    {
        var result = await _employeeService.DeleteEmployeeByIdAsync(id);
        return Ok(result);
    }
}