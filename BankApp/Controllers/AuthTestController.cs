using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthTestController : ControllerBase
{
    [HttpGet("Admin")]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> GetForAdminOnly()
    {
        return Ok("Success!");
    }

    [HttpGet("Employee")]
    [Authorize(Roles = "Employee")]
    public ActionResult<string> GetForEmployeeOnly()
    {
        return Ok("Success!");
    }

    [HttpGet("Customer")]
    [Authorize(Roles = "Customer")]
    public ActionResult<string> GetForCustomerOnly()
    {
        return Ok("Success!");
    }
}