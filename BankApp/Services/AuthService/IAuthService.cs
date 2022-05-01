using BankApp.Models.Requests;
using BankApp.Models.Responses;

namespace BankApp.Services.AuthService;

public interface IAuthService
{
    Task<AuthenticateResponse> AuthenticateAsync(LoginRequest request, string? ipAddress,
        CancellationToken cancellationToken = default);

    Task<AuthenticateResponse> RefreshTokenAsync(string token, string? ipAddress);
    Task<bool> RevokeRefreshTokenAsync(string token, string? ipAddress);
}