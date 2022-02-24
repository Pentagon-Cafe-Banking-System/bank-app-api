using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using BankApp.Models.Requests;
using BankApp.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.CustomerService;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserService _userService;

    public CustomerService(ApplicationDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var employees = await _dbContext.Customers.ToListAsync();
        return employees;
    }

    public async Task<Customer> GetCustomerByIdAsync(string id)
    {
        var customer = await _dbContext.Customers.FindAsync(id);
        if (customer == null)
            throw new NotFoundException("Customer not found");
        return customer;
    }

    public async Task<Customer> CreateCustomerAsync(CreateCustomerRequest request)
    {
        var user = new AppUser
        {
            UserName = request.UserName,
        };
        await _userService.CreateUserAsync(user, request.Password, "Customer");

        var customer = new Customer
        {
            AppUser = user
        };
        var entity = (await _dbContext.Customers.AddAsync(customer)).Entity;

        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IdentityResult> DeleteCustomerByIdAsync(string id)
    {
        var employee = await GetCustomerByIdAsync(id);
        var appUser = employee.AppUser;
        return await _userService.DeleteUserAsync(appUser);
    }
}