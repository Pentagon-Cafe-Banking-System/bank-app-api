﻿using AutoMapper;
using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions.RequestErrors;
using BankApp.Models;
using BankApp.Models.Requests;
using BankApp.Services.UserService;
using Microsoft.AspNetCore.Identity;

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

    public IEnumerable<Employee> GetAllEmployees()
    {
        var employees = _dbContext.Employees.AsEnumerable();
        return employees;
    }

    public async Task<Employee> GetEmployeeByIdAsync(string id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);
        if (employee == null)
            throw new NotFoundError("Id", "Employee with requested id could not be found");
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

            var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<CreateEmployeeRequest, Employee>()
            ));
            var employee = mapper.Map<Employee>(request);
            employee.AppUser = user;
            var employeeEntity = (await _dbContext.Employees.AddAsync(employee)).Entity;

            await _dbContext.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();

            return employeeEntity;
        }
    }

    public async Task<Employee> UpdateEmployeeByIdAsync(UpdateEmployeeRequest request, string id)
    {
        var hasher = new PasswordHasher<AppUser>();
        var employee = await GetEmployeeByIdAsync(id);
        employee.AppUser.UserName = request.UserName;
        employee.AppUser.NormalizedUserName = request.UserName.ToUpperInvariant();
        employee.AppUser.PasswordHash = hasher.HashPassword(null!, request.Password);
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Salary = request.Salary;
        await _dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<IdentityResult> DeleteEmployeeByIdAsync(string id)
    {
        return await _userService.DeleteUserByIdAsync(id);
    }
}