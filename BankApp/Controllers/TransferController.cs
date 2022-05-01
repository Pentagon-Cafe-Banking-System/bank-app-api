using System.Security.Claims;
using BankApp.Exceptions;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Models.Responses;
using BankApp.Services.AccountService;
using BankApp.Services.TransferService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/transfer-management")]
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
    [HttpGet("transfers")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<IList<TransferDto>>> GetAllTransfers()
    {
        var transfers = await _transferService.GetAllTransfersAsync();
        var transfersDto = transfers.Select(t => t.ToDto()).ToList();
        return Ok(transfersDto);
    }

    /// <summary>
    /// Returns all transfers of the authenticated customer sorted descending by order date. Only for customers.
    /// </summary>
    [HttpGet("customer/auth/transfers")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<IList<TransferDto>>> GetAllTransfersFromAndToCustomerByIdAsync()
    {
        var customerId = User.FindFirstValue(ClaimTypes.Sid);
        var transfers = await _transferService.GetAllTransfersFromAndToCustomerAsync(customerId);
        var transfersDto = transfers.Select(t => t.ToDto()).ToList();
        return Ok(transfersDto);
    }

    /// <summary>
    /// Returns transfer by id. Only for employees.
    /// </summary>
    [HttpGet("transfers/{transferId:long}")]
    [Authorize(Roles = RoleType.Employee)]
    public async Task<ActionResult<TransferDto>> GetTransferByIdAsync(long transferId)
    {
        var transfer = await _transferService.GetTransferByIdAsync(transferId);
        var transferDto = transfer.ToDto();
        return Ok(transferDto);
    }

    /// <summary>
    /// Creates a new transfer. Only for customers.
    /// </summary>
    [HttpPost("transfers")]
    [Authorize(Roles = RoleType.Customer)]
    public async Task<ActionResult<TransferDto>> CreateTransferAsync(CreateTransferRequest request)
    {
        var customerId = User.FindFirstValue(ClaimTypes.Sid);
        if (!await _accountService.IsCustomerAccountOwnerAsync(customerId, request.SenderAccountId))
            throw new ForbiddenException("Trying to send transfer from not owned account");
        var transfer = await _transferService.CreateTransferAsync(request);
        var transferDto = transfer.ToDto();
        return Ok(transferDto);
    }
}