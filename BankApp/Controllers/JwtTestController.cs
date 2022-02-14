using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JwtTestController : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> Get()
    {
        return Ok("Success!");
    }
}