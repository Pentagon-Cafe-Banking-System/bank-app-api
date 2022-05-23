using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.AdminService;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _dbContext;

    public AdminService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Admin>> GetAllAdminsAsync(CancellationToken cancellationToken = default)
    {
        var admins = await _dbContext.Admins.ToListAsync(cancellationToken: cancellationToken);
        return admins;
    }

    public async Task<Admin> GetAdminByIdAsync(string adminId, CancellationToken cancellationToken = default)
    {
        var admin = await _dbContext.Admins
            .FindAsync(new object?[] {adminId}, cancellationToken: cancellationToken);
        if (admin == null)
            throw new NotFoundException("Admin with requested id does not exist");
        return admin;
    }
}