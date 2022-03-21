namespace BankApp.Models.Responses;

public class AuthenticateResponse
{
    public AuthenticateResponse(string jwtToken, string refreshToken)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }

    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
}