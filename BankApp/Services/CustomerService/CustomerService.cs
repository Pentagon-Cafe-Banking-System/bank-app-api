using AutoMapper;
using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions.RequestExceptions;
using BankApp.Models;
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
            throw new NotFoundException(
                new RequestError("Id").Add("Customer with requested id could not be found")
            );
        return customer;
    }

    public async Task<Customer> CreateCustomerAsync(CreateCustomerRequest request)
    {
        await using (var dbContextTransaction = await _dbContext.Database.BeginTransactionAsync())
        {
            var user = new AppUser
            {
                UserName = request.UserName,
            };
            await _userService.CreateUserAsync(user, request.Password, RoleType.Customer);

            var mapper = new Mapper(
                new MapperConfiguration(cfg =>
                    cfg.CreateMap<CreateCustomerRequest, Customer>()
                )
            );
            var customer = mapper.Map<Customer>(request);
            customer.AppUser = user;

            var entity = (await _dbContext.Customers.AddAsync(customer)).Entity;

            await _dbContext.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();

            return entity;
        }
    }

    public async Task<IdentityResult> DeleteCustomerByIdAsync(string id)
    {
        var employee = await GetCustomerByIdAsync(id);
        var appUser = employee.AppUser;
        return await _userService.DeleteUserAsync(appUser);
    }
}