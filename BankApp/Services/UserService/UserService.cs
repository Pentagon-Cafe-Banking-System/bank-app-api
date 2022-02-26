using System.Security.Claims;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using BankApp.Exceptions.RequestExceptions;
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
        var users = _userManager.Users.ToList();
        return users;
    }

    public async Task<AppUser> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new NotFoundException(new RequestError
            {
                Code = "UserNotFound",
                Description = "User with requested id could not be found"
            });
        return user;
    }

    public async Task CreateUserAsync(AppUser user, string password, string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
            throw new AppException($"Role '{roleName}' does not exist");

        var createUserResult = await _userManager.CreateAsync(user, password);
        if (!createUserResult.Succeeded)
            throw new BadRequestException(createUserResult.Errors.ToList());

        await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> DeleteUserAsync(AppUser user)
    {
        return await _userManager.DeleteAsync(user);
    }

    public async Task<IdentityResult> DeleteUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new NotFoundException(new RequestError
            {
                Code = "UserNotFound",
                Description = "User with requested id could not be found"
            });
        return await _userManager.DeleteAsync(user);
    }

    public async Task<IEnumerable<Claim>> GetUserClaims(AppUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id)
        };
        var roles = (await _userManager.GetRolesAsync(user)).ToList();
        roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        return claims;
    }
}