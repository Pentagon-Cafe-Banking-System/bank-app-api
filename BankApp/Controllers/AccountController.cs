using BankApp.Entities;
using BankApp.Entities.UserTypes;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Employee)]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccountById(long id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);
        return Ok(account);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Account>> CreateAccount(CreateAccountRequest request)
    {
        var account = await _accountService.CreateAccountAsync(request);
        return Ok(account);
    }
    [HttpPatch("update/{id}")]
    public async Task<ActionResult<Account>> UpdateAccount(UpdateAccountRequest request,long id)
    {
        var account = await _accountService.UpdateAccountAsync(request,id);
        return Ok(account);
    }
}