using BankApp.Entities;
using BankApp.Entities.UserTypes;

namespace BankApp.Services.JwtService;

public interface IJwtService
{
    public Task<string> GenerateJwtTokenAsync(AppUser user);
    public Task<RefreshToken> GenerateRefreshTokenAsync(string? ipAddress);
}