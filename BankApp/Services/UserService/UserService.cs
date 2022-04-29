using System.Security.Claims;
using System.Transactions;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using BankApp.Exceptions.RequestErrors;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.UserService;

public class UserService : IUserService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IEnumerable<AppUser> GetAllUsers()
    {
        var users = _userManager.Users.AsEnumerable();
        return users;
    }

    public async Task<AppUser> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new NotFoundError("Id", "User with requested id could not be found");
        return user;
    }

    public async Task<IdentityResult> CreateUserAsync(AppUser user, string password, string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
            throw new AppException($"Role '{roleName}' does not exist");

        using (var scope = new TransactionScope())
        {
            var createUserIdentityResult = await _userManager.CreateAsync(user, password);
            if (!createUserIdentityResult.Succeeded)
                throw new AppException("UserManager could not create user");

            var addToRoleIdentityResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!addToRoleIdentityResult.Succeeded)
                throw new AppException("UserManager could not add user to role");

            scope.Complete();
            return addToRoleIdentityResult;
        }
    }

    public async Task<IdentityResult> DeleteUserAsync(AppUser user)
    {
        return await _userManager.DeleteAsync(user);
    }

    public async Task<IdentityResult> DeleteUserByIdAsync(string id)
    {
        var user = await GetUserByIdAsync(id);
        return await _userManager.DeleteAsync(user);
    }

    public async Task<IEnumerable<Claim>> GetUserClaimsAsync(AppUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id),
            new(ClaimTypes.Name, user.UserName),
        };
        var roles = (await _userManager.GetRolesAsync(user)).ToList();
        roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        return claims;
    }
}