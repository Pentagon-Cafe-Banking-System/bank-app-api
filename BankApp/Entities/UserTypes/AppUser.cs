using BankApp.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Entities.UserTypes;

public class AppUser : IdentityUser
{
    public virtual List<RefreshToken> RefreshTokens { get; set; } = new();

    public AppUserDto ToDto()
    {
        return new AppUserDto
        {
            Id = Id,
            UserName = UserName,
            NormalizedUserName = NormalizedUserName,
            TwoFactorEnabled = TwoFactorEnabled,
            LockoutEnd = LockoutEnd,
            LockoutEnabled = LockoutEnabled,
            AccessFailedCount = AccessFailedCount,
        };
    }
}