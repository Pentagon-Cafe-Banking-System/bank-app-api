using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BankApp.IntegrationTests.Helpers;

public class JwtServiceMock
{
    public JwtServiceMock()
    {
        Secret = Encoding.ASCII.GetBytes("VeryLongUltraSecureSecret");
        SecurityKey = new SymmetricSecurityKey(Secret);
    }

    private byte[] Secret { get; set; }
    public SymmetricSecurityKey SecurityKey { get; set; }

    public string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            notBefore: DateTime.UtcNow,
            signingCredentials: credentials
        );

        var jwt = tokenHandler.WriteToken(token);
        return jwt;
    }
}