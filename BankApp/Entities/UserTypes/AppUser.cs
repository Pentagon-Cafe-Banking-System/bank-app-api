using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Entities.UserTypes;

public class AppUser : IdentityUser
{
    [JsonIgnore] public virtual List<RefreshToken> RefreshTokens { get; set; } = default!;
}