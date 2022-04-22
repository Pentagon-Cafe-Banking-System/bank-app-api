using System.Security.Claims;
using BankApp.Entities;
using BankApp.Entities.UserTypes;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Services.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Customer>> GetCustomerById(string id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        return Ok(customer);
    }

    [HttpPost]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Customer>> CreateCustomer(CreateCustomerRequest request)
    {
        var customer = await _customerService.CreateCustomerAsync(request);
        return Ok(customer);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Customer>> UpdateCustomer(UpdateCustomerRequest request, string id)
    {
        var customer = await _customerService.UpdateCustomerAsync(request, id);
        return Ok(customer);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IdentityResult>> DeleteCustomerById(string id)
    {
        var result = await _customerService.DeleteCustomerByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("{id}/accounts")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsByCustomerId(string id)
    {
        var accounts = await _customerService.GetAllAccountsByCustomerIdAsync(id);
        return Ok(accounts);
    }

    [HttpGet("auth/accounts/")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsOfAuthenticatedCustomer()
    {
        var userId = User.FindFirstValue(ClaimTypes.Sid);
        var accounts = await _customerService.GetAllAccountsByCustomerIdAsync(userId);
        return Ok(accounts);
    }
}