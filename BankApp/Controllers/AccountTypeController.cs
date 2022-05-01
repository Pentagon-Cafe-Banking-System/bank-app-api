using BankApp.Entities;
using BankApp.Services.AccountTypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize]
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
    /// Returns all account types. For all authenticated users.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IList<AccountType>>> GetAllAccountTypesAsync()
    {
        var accountTypes = await _accountTypeService.GetAllAccountTypesAsync();
        return Ok(accountTypes);
    }

    /// <summary>
    /// Returns currencies available for specified account type. For all authenticated users.
    /// </summary>
    [HttpGet("{accountTypeId:int}/currencies")]
    public async Task<ActionResult<IList<Currency>>> GetCurrenciesOfAccountTypeAsync(int accountTypeId)
    {
        var currencies = await _accountTypeService.GetCurrenciesOfAccountTypeAsync(accountTypeId);
        return Ok(currencies);
    }
}