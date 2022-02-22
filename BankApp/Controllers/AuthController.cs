using BankApp.Exceptions;
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
        SetTokenCookie(response.RefreshToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (refreshToken is null)
            throw new NotFoundException("There is no refresh token in the request cookies");
        var response = await _authService.RefreshTokenAsync(refreshToken, IpAddress());
        SetTokenCookie(response.RefreshToken);
        return Ok(response);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken(string? token)
    {
        // accept refresh token in request body or cookie
        token ??= Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
            throw new BadRequestException("Token is required");

        await _authService.RevokeTokenAsync(token, IpAddress());
        return Ok(new {message = "Token revoked"});
    }

    // helper methods

    private void SetTokenCookie(string token)
    {
        // append cookie with refresh token to the http response
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7),
            // Properties below only for development
            Secure = true,
            SameSite = SameSiteMode.None,
            IsEssential = true
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }

    private string? IpAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? null;
    }
}