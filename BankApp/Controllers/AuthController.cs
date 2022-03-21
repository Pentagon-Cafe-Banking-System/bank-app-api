using BankApp.Models.Requests;
using BankApp.Models.Responses;
using BankApp.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(LoginRequest request)
    {
        var response = await _authService.AuthenticateAsync(request, IpAddress());
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticateResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var refreshToken = request.RefreshToken;
        var response = await _authService.RefreshTokenAsync(refreshToken, IpAddress());
        return Ok(response);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken(RevokeRefreshTokenRequest request)
    {
        var refreshToken = request.RefreshToken;
        await _authService.RevokeTokenAsync(refreshToken, IpAddress());
        return Ok(new {message = "Token revoked"});
    }

    // helper methods

    private string? IpAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? null;
    }
}