using AutoMapper;
using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions.RequestExceptions;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
            throw new NotFoundException(
                new RequestError("Id").Add("Employee with requested id could not be found")
            );
        return employee;
    }

    public async Task<Employee> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        await using (var dbContextTransaction = await _dbContext.Database.BeginTransactionAsync())
        {
            var user = new AppUser
            {
                UserName = request.UserName
            };
            await _userService.CreateUserAsync(user, request.Password, RoleType.Employee);

            var mapper = new Mapper(
                new MapperConfiguration(cfg =>
                    cfg.CreateMap<CreateEmployeeRequest, Employee>()
                )
            );
            var employee = mapper.Map<Employee>(request);
            employee.AppUser = user;
            
            var entity = (await _dbContext.Employees.AddAsync(employee)).Entity;

            await _dbContext.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();

            return entity;
        }
    }

    public async Task<Employee> UpdateEmployeeAsync(UpdateEmployeeRequest request,string id)
    {
        var hasher = new PasswordHasher<AppUser>();
        var employee = await GetEmployeeByIdAsync(id);
        employee.AppUser.UserName = request.UserName;
        employee.AppUser.NormalizedUserName = request.UserName.ToUpper();
        employee.AppUser.PasswordHash = hasher.HashPassword(null!, request.Password);
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Salary = request.Salary;
        await _dbContext.SaveChangesAsync();
        return employee;
    }
    public async Task<IdentityResult> DeleteEmployeeByIdAsync(string id)
    {
        var employee = await GetEmployeeByIdAsync(id);
        var appUser = employee.AppUser;
        return await _userService.DeleteUserAsync(appUser);
    }
}