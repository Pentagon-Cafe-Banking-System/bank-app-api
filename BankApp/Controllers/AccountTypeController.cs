using BankApp.Entities;
using BankApp.Models;
using BankApp.Services.AccountTypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Employee)]
[Route("api/account-types")]
[ApiExplorerSettings(GroupName = "Account types")]
public class AccountTypeController : ControllerBase
{
    private readonly IAccountTypeService _accountTypeService;

    public AccountTypeController(IAccountTypeService accountTypeService)
    {
        _accountTypeService = accountTypeService;
    }

    /// <summary>
    /// Returns all account types. Only for employees.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountType>>> GetAllAccountTypes()
    {
        var accountTypes = await _accountTypeService.GetAllAccountTypesAsync();
        return Ok(accountTypes);
    }

    /// <summary>
    /// Returns currencies available for specified account type. Only for employees.
    /// </summary>
    [HttpGet("{accountTypeId}/currencies")]
    public async Task<ActionResult<IEnumerable<Currency>>> GetCurrenciesByAccountTypeId(short accountTypeId)
    {
        var currencies = await _accountTypeService.GetCurrenciesByAccountTypeIdAsync(accountTypeId);
        return Ok(currencies);
    }
}