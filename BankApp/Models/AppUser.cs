using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Models;

public class AppUser : IdentityUser
{
    [JsonIgnore] public override string PasswordHash { get; set; } = string.Empty;
    [JsonIgnore] public List<RefreshToken> RefreshTokens { get; set; } = null!;
}