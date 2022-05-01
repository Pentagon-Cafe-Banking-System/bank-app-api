using System.Security.Claims;
using BankApp.Entities;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IList<AppUser>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userManager.Users.ToListAsync(cancellationToken: cancellationToken);
        return users;
    }

    public async Task<AppUser> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User with requested id does not exist");
        return user;
    }

    public async Task<AppUser> GetUserByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            throw new NotFoundException("User with requested username does not exist");
        return user;
    }

    public async Task<AppUser> CreateUserAsync(string userName, string password, string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
            throw new NotFoundException($"Role '{roleName}' does not exist");

        var user = new AppUser {UserName = userName};
        var createUserIdentityResult = await _userManager.CreateAsync(user, password);
        if (!createUserIdentityResult.Succeeded)
            throw new AppException("UserManager could not create user");

        var addToRoleIdentityResult = await _userManager.AddToRoleAsync(user, roleName);
        if (!addToRoleIdentityResult.Succeeded)
            throw new AppException("UserManager could not add user to role");

        return user;
    }

    public async Task<bool> DeleteUserByIdAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        var deleteIdentityResult = await _userManager.DeleteAsync(user);
        return deleteIdentityResult.Succeeded;
    }

    public async Task<IList<Claim>> GetUserRolesAsClaimsAsync(AppUser user)
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

    public async Task<IList<RefreshToken>> GetUserRefreshTokensAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        var refreshTokens = user.RefreshTokens.ToList();
        return refreshTokens;
    }

    public async Task<bool> UserNameExistsAsync(string userName, CancellationToken cancellationToken = default)
    {
        var exists = await _userManager.Users.AnyAsync(user =>
            user.NormalizedUserName == userName.ToUpper(), cancellationToken: cancellationToken);
        return exists;
    }

    public async Task<bool> ValidateUserPasswordAsync(string userName, string password)
    {
        var user = await GetUserByUserNameAsync(userName);
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        return isPasswordValid;
    }
}