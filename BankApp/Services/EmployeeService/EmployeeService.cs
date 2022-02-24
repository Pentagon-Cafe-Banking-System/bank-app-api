using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using BankApp.Models.Requests;
using BankApp.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.EmployeeService;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserService _userService;

    public EmployeeService(ApplicationDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        var employees = await _dbContext.Employees.ToListAsync();
        return employees;
    }

    public async Task<Employee> GetEmployeeByIdAsync(string id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);
        if (employee == null)
            throw new NotFoundException("Employee not found");
        return employee;
    }

    public async Task<Employee> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        var user = new AppUser
        {
            UserName = request.UserName,
        };
        await _userService.CreateUserAsync(user, request.Password, "Employee");

        var employee = new Employee
        {
            AppUser = user
        };
        var entity = (await _dbContext.Employees.AddAsync(employee)).Entity;

        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IdentityResult> DeleteEmployeeByIdAsync(string id)
    {
        var employee = await GetEmployeeByIdAsync(id);
        var appUser = employee.AppUser;
        return await _userService.DeleteUserAsync(appUser);
    }
}