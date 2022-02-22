using BankApp.Entities.UserTypes;
using BankApp.Models.Requests;
using BankApp.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
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
    public async Task<ActionResult<Employee>> GetEmployeeById(Int64 id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        return Ok(employee);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateEmployeeRequest request)
    {
        await _employeeService.CreateEmployeeAsync(request);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteById(Int64 id)
    {
        await _employeeService.DeleteEmployeeByIdAsync(id);
        return Ok();
    }
}