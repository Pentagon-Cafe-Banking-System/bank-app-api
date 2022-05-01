using System.Security.Claims;
using BankApp.Entities;
using BankApp.Entities.UserTypes;

namespace BankApp.Services.UserService;

public interface IUserService
{
    Task<IList<AppUser>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<AppUser> GetUserByIdAsync(string userId);
    Task<AppUser> GetUserByUserNameAsync(string userName);
    Task<AppUser> CreateUserAsync(string userName, string password, string roleName);
    Task<bool> DeleteUserByIdAsync(string userId);
    Task<IList<Claim>> GetUserRolesAsClaimsAsync(AppUser user);
    Task<IList<RefreshToken>> GetUserRefreshTokensAsync(string userId);
    Task<bool> UserNameExistsAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> ValidateUserPasswordAsync(string userName, string password);
}