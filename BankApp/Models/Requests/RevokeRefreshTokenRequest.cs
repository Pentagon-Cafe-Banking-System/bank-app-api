namespace BankApp.Models.Requests;

public class RevokeRefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}