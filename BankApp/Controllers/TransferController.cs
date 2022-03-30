using BankApp.Entities;
using BankApp.Models;
using BankApp.Models.Requests;
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

    public TransferController(ITransferService transferService)
    {
        _transferService = transferService;
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
        var transfer = await _transferService.CreateTransferAsync(request);
        return Ok(transfer);
    }
}