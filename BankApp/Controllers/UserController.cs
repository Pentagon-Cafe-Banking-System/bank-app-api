using BankApp.Entities;
using BankApp.Entities.UserTypes;
using BankApp.Models;
using BankApp.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize(Roles = RoleType.Admin)]
[Route("api/users")]
[ApiExplorerSettings(GroupName = "Users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Returns all base users. Only for admins.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Returns all refresh tokens for specified user. Only for admins.
    /// </summary>
    [HttpGet("{userId}/refresh-tokens")]
    public async Task<ActionResult<IEnumerable<RefreshToken>>> GetUserRefreshTokensAsync(string userId)
    {
        var refreshTokens = await _userService.GetUserRefreshTokensAsync(userId);
        return Ok(refreshTokens);
    }
}