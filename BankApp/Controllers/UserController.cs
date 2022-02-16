using BankApp.Models;
using BankApp.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUserById(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpGet("{id}/refresh-tokens")]
    public async Task<ActionResult<IEnumerable<RefreshToken>>> GetUserRefreshTokens(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user.RefreshTokens);
    }
}