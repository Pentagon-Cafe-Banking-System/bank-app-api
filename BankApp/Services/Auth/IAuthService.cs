using BankApp.Models;
using BankApp.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.Auth;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(AppUserDto userDto);
    Task<AuthenticateResponse> AuthenticateAsync(AppUserDto userDto, string? ipAddress);
    Task<AuthenticateResponse> RefreshTokenAsync(string token, string? ipAddress);
    Task<IdentityResult> RevokeTokenAsync(string token, string? ipAddress);
}