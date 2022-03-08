using BankApp.Entities;
using BankApp.Models;
using BankApp.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Employee)]
[Route("api/[controller]")]
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
}