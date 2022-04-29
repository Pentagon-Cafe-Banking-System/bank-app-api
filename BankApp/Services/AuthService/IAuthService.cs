using BankApp.Models.Requests;
using BankApp.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.AuthService;

public interface IAuthService
{
    Task<AuthenticateResponse> AuthenticateAsync(LoginRequest request, string? ipAddress);
    Task<AuthenticateResponse> RefreshTokenAsync(string token, string? ipAddress);
    Task<IdentityResult> RevokeRefreshTokenAsync(string token, string? ipAddress);
}