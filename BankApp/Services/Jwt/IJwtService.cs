using BankApp.Models;

namespace BankApp.Services.Jwt;

public interface IJwtService
{
    public Task<string> GenerateJwtTokenAsync(AppUser user);
    public Task<RefreshToken> GenerateRefreshToken(string? ipAddress);
}