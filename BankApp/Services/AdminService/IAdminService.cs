using BankApp.Entities.UserTypes;

namespace BankApp.Services.AdminService;

public interface IAdminService
{
    Task<IList<Admin>> GetAllAdminsAsync(CancellationToken cancellationToken = default);
    Task<Admin> GetAdminByIdAsync(string adminId, CancellationToken cancellationToken = default);
}