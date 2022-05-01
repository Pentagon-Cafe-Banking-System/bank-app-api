namespace BankApp.Models.Responses;

public class AppUserDto
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string NormalizedUserName { get; set; } = default!;
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}