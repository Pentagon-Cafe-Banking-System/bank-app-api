using BankApp.Entities.UserTypes;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Services.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerById(string id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        return Ok(customer);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Customer>> CreateCustomer(CreateCustomerRequest request)
    {
        var customer = await _customerService.CreateCustomerAsync(request);
        return Ok(customer);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<IdentityResult>> DeleteCustomerById(string id)
    {
        var result = await _customerService.DeleteCustomerByIdAsync(id);
        return Ok(result);
    }
}