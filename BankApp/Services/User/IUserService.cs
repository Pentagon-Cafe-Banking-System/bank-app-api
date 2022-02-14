using BankApp.Models;

namespace BankApp.Services.User;

public interface IUserService
{
    Task<IEnumerable<AppUser>> GetAllUsersAsync();
    Task<AppUser> GetUserByIdAsync(string id);
}