using BankApp.Entities.UserTypes;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Services.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Employee)]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllEmployees()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetEmployeeById(string id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        return Ok(customer);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCustomerRequest request)
    {
        await _customerService.CreateCustomerAsync(request);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        await _customerService.DeleteCustomerByIdAsync(id);
        return Ok();
    }
}