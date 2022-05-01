using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Entities.UserTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BankApp.Services.JwtService;

public class JwtService : IJwtService
{
    private readonly AppSettings _appSettings;
    private readonly UserManager<AppUser> _userManager;

    public JwtService(IOptions<AppSettings> appSettings, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    public string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.JwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            notBefore: DateTime.UtcNow,
            signingCredentials: credentials
        );

        var jwt = tokenHandler.WriteToken(token);
        return jwt;
    }

    public async Task<RefreshToken> GenerateRefreshTokenAsync(string? ipAddress,
        CancellationToken cancellationToken = default)
    {
        var refreshToken = new RefreshToken
        {
            Token = await GetUniqueToken(),
            // token is valid for 7 days
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };

        return refreshToken;

        async Task<string> GetUniqueToken()
        {
            string token;
            bool tokenIsUnique;

            do
            {
                // token is a cryptographically strong random sequence of values
                var localToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                token = localToken;
                // ensure token is unique by checking against db
                tokenIsUnique = !await _userManager.Users.AnyAsync(u =>
                        u.RefreshTokens.Any(t => t.Token == localToken), cancellationToken: cancellationToken
                );
            } while (!tokenIsUnique);

            return token;
        }
    }
}
// https://jasonwatmore.com/post/2022/01/24/net-6-jwt-authentication-with-refresh-tokens-tutorial-with-example-api