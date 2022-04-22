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
[Route("api/transfers")]
[ApiExplorerSettings(GroupName = "Transfers")]
public class TransferController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ITransferService _transferService;

    public TransferController(ITransferService transferService, IAccountService accountService)
    {
        _transferService = transferService;
        _accountService = accountService;
    }

    /// <summary>
    /// Returns all transfers. Only for employees.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IEnumerable<Transfer>>> GetAllTransfers()
    {
        var transfers = await _transferService.GetAllTransfersAsync();
        return Ok(transfers);
    }

    /// <summary>
    /// Returns all transfers of the authenticated customer sorted descending by order date. Only for customers.
    /// </summary>
    [HttpGet("customer/auth")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<IEnumerable<Transfer>>> GetAllCustomerTransfers()
    {
        var customerId = User.FindFirstValue(ClaimTypes.Sid);
        var transfers = await _transferService.GetAllTransfersFromAndToCustomerAsync(customerId);
        return Ok(transfers);
    }

    /// <summary>
    /// Returns transfer by id. Only for employees.
    /// </summary>
    [HttpGet("{transferId}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<Transfer>> GetTransferById(long transferId)
    {
        var transfer = await _transferService.GetTransferByIdAsync(transferId);
        return Ok(transfer);
    }

    /// <summary>
    /// Creates a new transfer. Only for customers.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<Transfer>> CreateTransfer(CreateTransferRequest request)
    {
        var customerId = User.FindFirstValue(ClaimTypes.Sid);
        if (!await _accountService.IsCustomerAccountOwnerAsync(customerId, request.SenderAccountId))
            throw new BadRequestException("SenderAccountId", "Trying to send transfer from not owned account");
        var transfer = await _transferService.CreateTransferAsync(request);
        return Ok(transfer);
    }
}