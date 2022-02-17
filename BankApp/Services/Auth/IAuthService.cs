using BankApp.Models.Reponses;
using BankApp.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.Auth;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest request);
    Task<AuthenticateResponse> AuthenticateAsync(LoginRequest request, string? ipAddress);
    Task<AuthenticateResponse> RefreshTokenAsync(string token, string? ipAddress);
    Task<IdentityResult> RevokeTokenAsync(string token, string? ipAddress);
}