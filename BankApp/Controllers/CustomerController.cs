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
[ApiExplorerSettings(GroupName = "Customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// Returns all customers. Only for employees.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomersAsync()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    /// <summary>
    /// Returns customer by id. Only for employees.
    /// </summary>
    [HttpGet("{customerId}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Customer>> GetCustomerByIdAsync(string customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        return Ok(customer);
    }

    /// <summary>
    /// Creates new customer. Only for employees.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Customer>> CreateCustomerAsync(CreateCustomerRequest request)
    {
        var customer = await _customerService.CreateCustomerAsync(request);
        return Ok(customer);
    }

    /// <summary>
    /// Updates customer by id. Only for employees.
    /// </summary>
    [HttpPatch("{customerId}")] // TODO - make it true PATCH
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Customer>> UpdateCustomerByIdAsync(UpdateCustomerRequest request, string customerId)
    {
        var customer = await _customerService.UpdateCustomerByIdAsync(request, customerId);
        return Ok(customer);
    }

    /// <summary>
    /// Deletes customer by id. Only for employees.
    /// </summary>
    [HttpDelete("{customerId}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IdentityResult>> DeleteCustomerByIdAsync(string customerId)
    {
        var result = await _customerService.DeleteCustomerByIdAsync(customerId);
        return Ok(result);
    }
}