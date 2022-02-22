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

    public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return users;
    }

    public async Task<AppUser> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new NotFoundException("User not found");
        return user;
    }

    public async Task CreateUserAsync(AppUser user, string password, string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
            throw new BadRequestException($"Role '{roleName}' does not exist");

        var createUserResult = await _userManager.CreateAsync(user, password);
        if (!createUserResult.Succeeded)
            throw new BadRequestException("Could not create user");

        await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task DeleteUserAsync(AppUser user)
    {
        await _userManager.DeleteAsync(user);
    }

    public async Task DeleteUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            throw new NotFoundException("User not found");
        await _userManager.DeleteAsync(user);
    }
}