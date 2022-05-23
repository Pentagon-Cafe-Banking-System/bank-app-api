using BankApp.Entities.UserTypes;
using BankApp.Models;
using BankApp.Services.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Admin)]
[Route("api/admin-management")]
[ApiExplorerSettings(GroupName = "Admins")]

public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    /// <summary>
    /// Returns all admins. Only for admins.
    /// </summary>
    [HttpGet("admins")]
    public async Task<ActionResult<IList<Admin>>> GetAllAdminsAsync()
    {
        var admins = await _adminService.GetAllAdminsAsync();
        var adminsDto = admins.Select(a => a.ToDto()).ToList();
        return Ok(adminsDto);
    }

    /// <summary>
    /// Returns employee by id. Only for admins.
    /// </summary>
    [HttpGet("admin/{adminId}")]
    public async Task<ActionResult<Admin>> GetEmployeeByIdAsync(string adminId)
    {
        var admin = await _adminService.GetAdminByIdAsync(adminId);
        var adminDto = admin.ToDto();
        return Ok(adminDto);
    }
}