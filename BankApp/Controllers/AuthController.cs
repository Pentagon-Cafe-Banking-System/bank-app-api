using BankApp.Models.Requests;
using BankApp.Models.Responses;
using BankApp.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
[ApiExplorerSettings(GroupName = "Authentication")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Returns a pair of access token and refresh token by username and password.
    /// </summary>
    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(LoginRequest request)
    {
        var response = await _authService.AuthenticateAsync(request, IpAddress());
        return Ok(response);
    }

    /// <summary>
    /// Returns a new pair of access token and refresh token or the user by using the refresh token.
    /// </summary>
    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticateResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var refreshToken = request.RefreshToken;
        var response = await _authService.RefreshTokenAsync(refreshToken, IpAddress());
        return Ok(response);
    }

    /// <summary>
    /// Revokes specified refresh token.
    /// </summary>
    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken(RevokeRefreshTokenRequest request)
    {
        var refreshToken = request.RefreshToken;
        await _authService.RevokeTokenAsync(refreshToken, IpAddress());
        return Ok(new {message = "Token revoked"});
    }

    private string? IpAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? null;
    }
}