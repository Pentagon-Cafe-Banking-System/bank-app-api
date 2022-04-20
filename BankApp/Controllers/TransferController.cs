using System.Security.Claims;
using BankApp.Entities;
using BankApp.Exceptions.RequestExceptions;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Services.AccountService;
using BankApp.Services.TransferService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Customer)]
[Route("api/[controller]")]

public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;
    private readonly IAccountService _accountService;

    public TransferController(ITransferService transferService, IAccountService accountService)
    {
        _transferService = transferService;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transfer>>> GetAllTransfers()
    {
        var transfers = await _transferService.GetAllTransfersAsync();
        return Ok(transfers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transfer>> GetTransferById(long id)
    {
        var transfer = await _transferService.GetTransferByIdAsync(id);
        return Ok(transfer);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Transfer>> CreateTransfer(CreateTransferRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.Sid);
        if (!await _accountService.IsUserAccountOwnerAsync(userId, request.AccountId))
            throw new BadRequestException("AccountId", "Trying to do transfer from another user's account");
        var transfer = await _transferService.CreateTransferAsync(request);
        return Ok(transfer);
    }
}