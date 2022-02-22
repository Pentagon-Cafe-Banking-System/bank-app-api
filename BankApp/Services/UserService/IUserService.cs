using BankApp.Entities.UserTypes;

namespace BankApp.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<AppUser>> GetAllUsersAsync();
    Task<AppUser> GetUserByIdAsync(string id);
    Task CreateUserAsync(AppUser user, string password, string roleName);
    Task DeleteUserAsync(AppUser user);
    Task DeleteUserByIdAsync(string id);
}