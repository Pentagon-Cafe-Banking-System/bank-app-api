﻿using BankApp.Entities;
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
    public ActionResult<IEnumerable<AccountType>> GetAllAccountTypes()
    {
        var accountTypes = _accountTypeService.GetAllAccountTypes();
        return Ok(accountTypes);
    }

    /// <summary>
    /// Returns currencies available for specified account type. For all authenticated users.
    /// </summary>
    [HttpGet("{accountTypeId}/currencies")]
    public ActionResult<IEnumerable<Currency>> GetCurrenciesByAccountTypeId(short accountTypeId)
    {
        var currencies = _accountTypeService.GetCurrenciesByAccountTypeId(accountTypeId);
        return Ok(currencies);
    }
}