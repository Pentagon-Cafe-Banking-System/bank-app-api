using BankApp.Entities.UserTypes;

namespace BankApp.Services.UserService;

public interface IUserService
{
    IEnumerable<AppUser> GetAllUsers();
    Task<AppUser> GetUserByIdAsync(string id);
    Task CreateUserAsync(AppUser user, string password, string roleName);
    Task DeleteUserAsync(AppUser user);
    Task DeleteUserByIdAsync(string id);
}