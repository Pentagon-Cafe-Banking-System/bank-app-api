using System.Security.Claims;
using BankApp.Exceptions;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Models.Responses;
using BankApp.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/account-management")]
[ApiExplorerSettings(GroupName = "Accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// Returns all accounts. Only for employees.
    /// </summary>
    [HttpGet("accounts")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IList<AccountDto>>> GetAllAccountsAsync()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        var accountsDto = accounts.Select(a => a.ToDto()).ToList();
        return Ok(accountsDto);
    }

    /// <summary>
    /// Returns account by id. Only for employees.
    /// </summary>
    [HttpGet("accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<AccountDto>> GetAccountByIdAsync(long accountId)
    {
        var account = await _accountService.GetAccountByIdAsync(accountId);
        var accountDto = account.ToDto();
        return Ok(accountDto);
    }

    /// <summary>
    /// Returns all accounts of authenticated customer. Only for customers.
    /// </summary>
    [HttpGet("customers/auth/accounts")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<IList<AccountDto>>> GetAllAccountsOfAuthenticatedCustomerAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.Sid);
        var accounts = await _accountService.GetAccountsByCustomerIdAsync(userId);
        var accountsDto = accounts.Select(a => a.ToDto()).ToList();
        return Ok(accountsDto);
    }

    /// <summary>
    /// Returns all accounts of the specified customer. Only for employees.
    /// </summary>
    [HttpGet("customers/{customerId}/accounts")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IList<AccountDto>>> GetAccountsByCustomerIdAsync(string customerId)
    {
        var accounts = await _accountService.GetAccountsByCustomerIdAsync(customerId);
        var accountsDto = accounts.Select(a => a.ToDto()).ToList();
        return Ok(accountsDto);
    }

    /// <summary>
    /// Returns account of the authenticated customer by id. Only for customers.
    /// </summary>
    [HttpGet("customers/auth/accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<IList<AccountDto>>> GetAccountByIdOfAuthenticatedCustomerAsync(long accountId)
    {
        var customerId = User.FindFirstValue(ClaimTypes.Sid);
        if (!await _accountService.IsCustomerAccountOwnerAsync(customerId, accountId))
            throw new ForbiddenException("Trying to access account that is not owned by the customer");
        var account = await _accountService.GetAccountByIdAsync(accountId);
        var accountDto = account.ToDto();
        return Ok(accountDto);
    }

    /// <summary>
    /// Creates new account. Only for employees.
    /// </summary>
    [HttpPost("accounts")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<AccountDto>> CreateAccountAsync(CreateAccountRequest request)
    {
        var account = await _accountService.CreateAccountAsync(request);
        var accountDto = account.ToDto();
        return Ok(accountDto);
    }

    /// <summary>
    /// Updates account by id. Only for employees.
    /// </summary>
    [HttpPatch("accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<AccountDto>> UpdateAccountByEmployeeAsync(UpdateAccountRequest request,
        long accountId)
    {
        var account = await _accountService.UpdateAccountAsync(request, accountId);
        var accountDto = account.ToDto();
        return Ok(accountDto);
    }

    /// <summary>
    /// Updates account of the authenticated customer by id. Only for customers.
    /// </summary>
    [HttpPatch("customers/auth/accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<AccountDto>> UpdateAccountByCustomerAsync(UpdateAccountRequest request,
        long accountId)
    {
        var customerId = User.FindFirstValue(ClaimTypes.Sid);
        if (!await _accountService.IsCustomerAccountOwnerAsync(customerId, accountId))
            throw new ForbiddenException("Trying to access account that is not owned by the customer");
        var account = await _accountService.UpdateAccountAsync(request, accountId);
        var accountDto = account.ToDto();
        return Ok(accountDto);
    }

    /// <summary>
    /// Deletes account by id. Only for employees.
    /// </summary>
    [HttpDelete("accounts/{accountId:long}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<IActionResult> DeleteAccountByIdAsync(long accountId)
    {
        await _accountService.DeleteAccountByIdAsync(accountId);
        return Ok();
    }
}