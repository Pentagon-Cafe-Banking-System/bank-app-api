using System.Security.Claims;
using BankApp.Entities;
using BankApp.Exceptions.RequestErrors;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Services.AccountService;
using BankApp.Services.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api")]
[ApiExplorerSettings(GroupName = "Accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ICustomerService _customerService;

    public AccountController(IAccountService accountService, ICustomerService customerService)
    {
        _accountService = accountService;
        _customerService = customerService;
    }

    /// <summary>
    /// Returns all accounts. Only for employees.
    /// </summary>
    [HttpGet("accounts")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        return Ok(accounts);
    }

    /// <summary>
    /// Returns account by id. Only for employees.
    /// </summary>
    [HttpGet("accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Account>> GetAccountById(long accountId)
    {
        var account = await _accountService.GetAccountByIdAsync(accountId);
        return Ok(account);
    }

    /// <summary>
    /// Returns all accounts of authenticated customer. Only for customers.
    /// </summary>
    [HttpGet("customers/auth/accounts")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsOfAuthenticatedCustomer()
    {
        var userId = User.FindFirstValue(ClaimTypes.Sid);
        var accounts = await _customerService.GetAllAccountsByCustomerIdAsync(userId);
        return Ok(accounts);
    }

    /// <summary>
    /// Returns all accounts of the specified customer. Only for employees.
    /// </summary>
    [HttpGet("customers/{customerId}/accounts")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsByCustomerId(string customerId)
    {
        var accounts = await _customerService.GetAllAccountsByCustomerIdAsync(customerId);
        return Ok(accounts);
    }

    /// <summary>
    /// Returns account of the authenticated customer by id. Only for customers.
    /// </summary>
    [HttpGet("customers/auth/accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccountByIdOfAuthenticatedCustomer(long accountId)
    {
        var customerId = User.FindFirstValue(ClaimTypes.Sid);
        if (!await _accountService.IsCustomerAccountOwnerAsync(customerId, accountId))
            throw new BadRequestError("AccountId", "Trying to access account that is not owned by the customer.");
        var account = await _accountService.GetAccountByIdAsync(accountId);
        return Ok(account);
    }

    /// <summary>
    /// Creates new account. Only for employees.
    /// </summary>
    [HttpPost("accounts")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Account>> CreateAccount(CreateAccountRequest request)
    {
        var account = await _accountService.CreateAccountAsync(request);
        return Ok(account);
    }

    /// <summary>
    /// Updates account by id. Only for employees.
    /// </summary>
    [HttpPatch("accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Account>> UpdateAccountByEmployee(UpdateAccountRequest request, long accountId)
    {
        var account = await _accountService.UpdateAccountAsync(request, accountId);
        return Ok(account);
    }

    /// <summary>
    /// Updates account of the authenticated customer by id. Only for customers.
    /// </summary>
    [HttpPatch("customers/auth/accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<Account>> UpdateAccountByCustomer(UpdateAccountRequest request, long accountId)
    {
        var customerId = User.FindFirstValue(ClaimTypes.Sid);
        if (!await _accountService.IsCustomerAccountOwnerAsync(customerId, accountId))
            throw new BadRequestError("AccountId", "Trying to access account that is not owned by the customer.");
        var account = await _accountService.UpdateAccountAsync(request, accountId);
        return Ok(account);
    }

    /// <summary>
    /// Deletes account by id. Only for employees.
    /// </summary>
    [HttpDelete("accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<IActionResult> DeleteAccount(long accountId)
    {
        await _accountService.DeleteAccountAsync(accountId);
        return Ok();
    }
}