using System.Security.Claims;
using BankApp.Entities;

namespace BankApp.Services.JwtService;

public interface IJwtService
{
    public string GenerateJwtToken(IEnumerable<Claim> claims);
    public Task<RefreshToken> GenerateRefreshTokenAsync(string? ipAddress);
}