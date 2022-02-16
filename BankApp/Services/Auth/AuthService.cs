using BankApp.Data;
using BankApp.Exceptions;
using BankApp.Models;
using BankApp.Models.DTO;
using BankApp.Utils.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BankApp.Services.Auth;

public class AuthService : IAuthService
{
    private readonly AppSettings _appSettings;
    private readonly IJwtUtils _jwtUtils;
    private readonly UserManager<AppUser> _userManager;

    public AuthService(IOptions<AppSettings> appSettings, UserManager<AppUser> userManager, IJwtUtils jwtUtils)
    {
        _appSettings = appSettings.Value;
        _userManager = userManager;
        _jwtUtils = jwtUtils;
    }

    public async Task<IdentityResult> RegisterAsync(AppUserDto userDto)
    {
        var user = new AppUser
        {
            UserName = userDto.UserName,
        };
        return await _userManager.CreateAsync(user, userDto.Password);
    }

    public async Task<AuthenticateResponse> AuthenticateAsync(AppUserDto userDto, string? ipAddress)
    {
        var user = await _userManager.FindByNameAsync(userDto.UserName);
        if (user is null)
            throw new NotFoundException("Username does not exist");

        var passwordCheck = await _userManager.CheckPasswordAsync(user, userDto.Password);
        if (!passwordCheck)
            throw new NotFoundException("Incorrect password");

        var jwtToken = _jwtUtils.GenerateJwtToken(user);
        var refreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);

        // remove old refresh tokens from user
        RemoveOldRefreshTokens(user);

        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        return new AuthenticateResponse(jwtToken, refreshToken.Token);
    }

    public async Task<AuthenticateResponse> RefreshTokenAsync(string token, string? ipAddress)
    {
        var user = await GetUserByRefreshTokenAsync(token);
        var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        if (refreshToken.IsRevoked)
        {
            // revoke all descendant tokens in case this token has been compromised
            RevokeDescendantRefreshTokens(refreshToken, user, ipAddress,
                $"Attempted reuse of revoked ancestor token: {token}");
            await _userManager.UpdateAsync(user);
        }

        if (!refreshToken.IsActive)
            throw new NotFoundException("Invalid token");

        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = await RotateRefreshTokenAsync(refreshToken, ipAddress);
        user.RefreshTokens.Add(newRefreshToken);

        // remove old refresh tokens from user
        RemoveOldRefreshTokens(user);

        // save changes to db
        await _userManager.UpdateAsync(user);

        // generate new jwt
        var jwtToken = _jwtUtils.GenerateJwtToken(user);

        return new AuthenticateResponse(jwtToken, newRefreshToken.Token);
    }

    public async Task<IdentityResult> RevokeTokenAsync(string token, string? ipAddress)
    {
        var user = await GetUserByRefreshTokenAsync(token);
        var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        if (!refreshToken.IsActive)
            throw new NotFoundException("Invalid token");

        // revoke token and save
        RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
        return await _userManager.UpdateAsync(user);
    }


    // helper methods

    private async Task<AppUser> GetUserByRefreshTokenAsync(string token)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
        if (user is null)
            throw new NotFoundException("Invalid token");

        return user;
    }

    private async Task<RefreshToken> RotateRefreshTokenAsync(RefreshToken refreshToken, string? ipAddress)
    {
        var newRefreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
        RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    private void RemoveOldRefreshTokens(AppUser user)
    {
        // remove old inactive refresh tokens from user based on TTL in app settings
        user.RefreshTokens.RemoveAll(x =>
            !x.IsActive && x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
    }

    private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, AppUser user, string? ipAddress,
        string reason)
    {
        if (string.IsNullOrEmpty(refreshToken.ReplacedByToken)) return;

        // recursively traverse the refresh token chain and ensure all descendants are revoked
        var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
        if (childToken is null) return;

        if (childToken.IsActive)
            RevokeRefreshToken(childToken, ipAddress, reason);
        else
            RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
    }

    private void RevokeRefreshToken(RefreshToken token, string? ipAddress, string? reason = null,
        string? replacedByToken = null)
    {
        token.Revoked = DateTime.UtcNow;
        token.RevokedByIp = ipAddress;
        token.ReasonRevoked = reason;
        token.ReplacedByToken = replacedByToken;
    }
}