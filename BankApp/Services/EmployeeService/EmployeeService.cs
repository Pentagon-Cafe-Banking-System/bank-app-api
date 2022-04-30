using AutoMapper;
using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using BankApp.Models;
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

    public async Task<IList<Employee>> GetAllEmployeesAsync()
    {
        var employees = await _dbContext.Employees.ToListAsync();
        return employees;
    }

    public async Task<Employee> GetEmployeeByIdAsync(string employeeId)
    {
        var employee = await _dbContext.Employees.FindAsync(employeeId);
        if (employee == null)
            throw new NotFoundException("Employee with requested id does not exist");
        return employee;
    }

    public async Task<Employee> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        var user = await _userService.CreateUserAsync(request.UserName, request.Password, RoleType.Employee);

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.CreateMap<CreateEmployeeRequest, Employee>()
        ));
        var employee = mapper.Map<Employee>(request);
        employee.AppUser = user;

        var employeeEntity = (await _dbContext.Employees.AddAsync(employee)).Entity;
        await _dbContext.SaveChangesAsync();
        return employeeEntity;
    }

    public async Task<Employee> UpdateEmployeeByIdAsync(UpdateEmployeeRequest request, string employeeId)
    {
        var hasher = new PasswordHasher<AppUser>();
        var employee = await GetEmployeeByIdAsync(employeeId);
        employee.AppUser.UserName = request.UserName;
        employee.AppUser.NormalizedUserName = request.UserName.ToUpperInvariant();
        employee.AppUser.PasswordHash = hasher.HashPassword(null!, request.Password);
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Salary = request.Salary;
        await _dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> DeleteEmployeeByIdAsync(string employeeId)
    {
        return await _userService.DeleteUserByIdAsync(employeeId);
    }
}