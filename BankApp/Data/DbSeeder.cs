using BankApp.Entities.UserTypes;
using BankApp.Models;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Data;

public class DbSeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<AppUser> _userManager;

    public DbSeeder(UserManager<AppUser> userManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task Seed()
    {
        // Create roles
        if (!_dbContext.Roles.IsNullOrEmpty())
            return;

        var roles = new List<IdentityRole>
        {
            new() {Name = RoleType.Admin, NormalizedName = RoleType.Admin.ToUpperInvariant()},
            new() {Name = RoleType.Employee, NormalizedName = RoleType.Employee.ToUpperInvariant()},
            new() {Name = RoleType.Customer, NormalizedName = RoleType.Customer.ToUpperInvariant()}
        };
        await _dbContext.Roles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        // Create admin account
        var hasher = new PasswordHasher<AppUser>();
        var adminAppUser = new AppUser
        {
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            PasswordHash = hasher.HashPassword(null!, "admin")
        };
        var entity = (await _dbContext.Users.AddAsync(adminAppUser)).Entity;
        await _dbContext.SaveChangesAsync();
        await _userManager.AddToRoleAsync(entity, "Admin");

        var admin = new Admin
        {
            AppUser = adminAppUser
        };
        await _dbContext.Admins.AddAsync(admin);

        await _dbContext.SaveChangesAsync();
    }
}