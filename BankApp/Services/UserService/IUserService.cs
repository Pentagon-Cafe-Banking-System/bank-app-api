using System.Security.Claims;
using BankApp.Entities.UserTypes;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Services.UserService;

public interface IUserService
{
    IEnumerable<AppUser> GetAllUsers();
    Task<AppUser> GetUserByIdAsync(string id);
    Task<AppUser> GetUserByUserNameAsync(string userName);
    Task<IdentityResult> CreateUserAsync(AppUser user, string password, string roleName);
    Task<IdentityResult> DeleteUserAsync(AppUser user);
    Task<IdentityResult> DeleteUserByIdAsync(string id);
    Task<IEnumerable<Claim>> GetUserClaimsAsync(AppUser user);
}