using BankApp.Entities;
using BankApp.Models;
using BankApp.Models.Responses;
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
    public async Task<ActionResult<IList<AppUserDto>>> GetAllUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        var usersDto = users.Select(u => u.ToDto()).ToList();
        return Ok(usersDto);
    }

    /// <summary>
    /// Returns all refresh tokens for specified user. Only for admins.
    /// </summary>
    [HttpGet("{userId}/refresh-tokens")]
    public async Task<ActionResult<IList<RefreshToken>>> GetUserRefreshTokensAsync(string userId)
    {
        var refreshTokens = await _userService.GetUserRefreshTokensAsync(userId);
        return Ok(refreshTokens);
    }
}