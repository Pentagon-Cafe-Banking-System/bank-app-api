using BankApp.Entities;
using BankApp.Models;
using BankApp.Services.AccountTypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Employee)]
[Route("api/account-types")]
public class AccountTypeController : ControllerBase
{
    private readonly IAccountTypeService _accountTypeService;

    public AccountTypeController(IAccountTypeService accountTypeService)
    {
        _accountTypeService = accountTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountType>>> GetAllAccountTypes()
    {
        var accountTypes = await _accountTypeService.GetAllAccountTypesAsync();
        return Ok(accountTypes);
    }

    [HttpGet("{id}/currencies")]
    public async Task<ActionResult<IEnumerable<Currency>>> GetCurrenciesByAccountTypeId(short id)
    {
        var currencies = await _accountTypeService.GetCurrenciesByAccountTypeIdAsync(id);
        return Ok(currencies);
    }
}