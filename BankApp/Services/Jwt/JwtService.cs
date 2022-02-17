using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BankApp.Data;
using BankApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BankApp.Services.Jwt;

public class JwtService : IJwtService
{
    private readonly AppSettings _appSettings;
    private readonly UserManager<AppUser> _userManager;

    public JwtService(IOptions<AppSettings> appSettings, UserManager<AppUser> userManager)
    {
        _appSettings = appSettings.Value;
        _userManager = userManager;
    }

    public async Task<string> GenerateJwtTokenAsync(AppUser user)
    {
        List<Claim> claims = new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id)
        };
        var roles = (await _userManager.GetRolesAsync(user)).ToList();
        roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

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

    public async Task<RefreshToken> GenerateRefreshToken(string? ipAddress)
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
                token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                // ensure token is unique by checking against db
                tokenIsUnique = !await _userManager.Users.AnyAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            } while (!tokenIsUnique);

            return token;
        }
    }
}
// https://jasonwatmore.com/post/2022/01/24/net-6-jwt-authentication-with-refresh-tokens-tutorial-with-example-api