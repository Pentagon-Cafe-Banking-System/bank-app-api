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

    public async Task<IList<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
    {
        var employees = await _dbContext.Employees.ToListAsync(cancellationToken: cancellationToken);
        return employees;
    }

    public async Task<Employee> GetEmployeeByIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        var employee = await _dbContext.Employees
            .FindAsync(new object?[] {employeeId}, cancellationToken: cancellationToken);
        if (employee == null)
            throw new NotFoundException("Employee with requested id does not exist");
        return employee;
    }

    public async Task<Employee> CreateEmployeeAsync(CreateEmployeeRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userService.CreateUserAsync(request.UserName, request.Password, RoleType.Employee);

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.CreateMap<CreateEmployeeRequest, Employee>()
        ));
        var employee = mapper.Map<Employee>(request);
        employee.AppUser = user;

        var employeeEntity = (await _dbContext.Employees.AddAsync(employee, cancellationToken)).Entity;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return employeeEntity;
    }

    public async Task<Employee> UpdateEmployeeByIdAsync(UpdateEmployeeRequest request, string employeeId,
        CancellationToken cancellationToken = default)
    {
        var hasher = new PasswordHasher<AppUser>();
        var employee = await GetEmployeeByIdAsync(employeeId, cancellationToken);
        employee.AppUser.UserName = request.UserName;
        employee.AppUser.NormalizedUserName = request.UserName.ToUpper();
        employee.AppUser.PasswordHash = hasher.HashPassword(null!, request.Password);
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Salary = request.Salary;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return employee;
    }

    public async Task<bool> DeleteEmployeeByIdAsync(string employeeId)
    {
        return await _userService.DeleteUserByIdAsync(employeeId);
    }
}