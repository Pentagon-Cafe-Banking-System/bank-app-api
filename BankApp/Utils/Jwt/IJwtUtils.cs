using BankApp.Models;

namespace BankApp.Utils.Jwt;

public interface IJwtUtils
{
    public string GenerateJwtToken(AppUser user);
    public Task<RefreshToken> GenerateRefreshToken(string? ipAddress);
}