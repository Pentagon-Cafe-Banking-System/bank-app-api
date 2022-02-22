using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using BankApp.Models.Requests;
using BankApp.Services.UserService;
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

    public async Task<Customer> GetCustomerByIdAsync(long id)
    {
        var customer = await _dbContext.Customers.FindAsync(id);
        if (customer == null)
            throw new NotFoundException("Employee not found");
        return customer;
    }

    public async Task CreateCustomerAsync(CreateCustomerRequest request)
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
        await _dbContext.Customers.AddAsync(customer);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCustomerByIdAsync(long id)
    {
        var employee = await GetCustomerByIdAsync(id);
        var appUser = employee.AppUser;
        await _userService.DeleteUserAsync(appUser);
    }
}