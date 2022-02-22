using System.Text.Json.Serialization;

namespace BankApp.Models.Responses;

public class AuthenticateResponse
{
    public AuthenticateResponse(string jwtToken, string refreshToken)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }

    public string JwtToken { get; set; }
    [JsonIgnore] public string RefreshToken { get; set; } // refresh token is returned in http only cookie
}