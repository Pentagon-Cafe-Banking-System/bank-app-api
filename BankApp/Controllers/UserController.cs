﻿using System.Security.Claims;
using BankApp.Entities;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using BankApp.Models;
using BankApp.Services.UserService;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = RoleType.Admin)]
    public ActionResult<IEnumerable<AppUser>> GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<AppUser>> GetUserById(string id)
    // {
    //     var user = await _userService.GetUserByIdAsync(id);
    //     return Ok(user);
    // }

    [HttpGet("{id}/refresh-tokens")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<RefreshToken>>> GetUserRefreshTokens(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.Sid);
        if (userId != id)
            throw new BadRequestException("Unauthorized access");

        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user.RefreshTokens);
    }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteUserById(string id)
    // {
    //     await _userService.DeleteUserByIdAsync(id);
    //     return Ok();
    // }
}