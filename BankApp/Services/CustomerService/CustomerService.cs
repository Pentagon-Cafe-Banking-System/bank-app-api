using AutoMapper;
using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
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

    public async Task<IList<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default)
    {
        var employees = await _dbContext.Customers.ToListAsync(cancellationToken: cancellationToken);
        return employees;
    }

    public async Task<Customer> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .FindAsync(new object?[] {customerId}, cancellationToken: cancellationToken);
        if (customer == null)
            throw new NotFoundException("Customer with requested id does not exist");
        return customer;
    }

    public async Task<Customer> CreateCustomerAsync(CreateCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userService.CreateUserAsync(request.UserName, request.Password, RoleType.Customer);

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.CreateMap<CreateCustomerRequest, Customer>()
        ));
        var customer = mapper.Map<Customer>(request);
        customer.AppUser = user;

        var customerEntity = (await _dbContext.Customers.AddAsync(customer, cancellationToken)).Entity;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return customerEntity;
    }

    public async Task<Customer> UpdateCustomerByIdAsync(UpdateCustomerRequest request, string customerId,
        CancellationToken cancellationToken = default)
    {
        var hasher = new PasswordHasher<AppUser>();
        var customer = await GetCustomerByIdAsync(customerId, cancellationToken);
        customer.AppUser.UserName = request.UserName;
        customer.AppUser.NormalizedUserName = request.UserName.ToUpper();
        customer.AppUser.PasswordHash = hasher.HashPassword(null!, request.Password);
        customer.FirstName = request.FirstName;
        customer.MiddleName = request.SecondName;
        customer.LastName = request.LastName;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return customer;
    }

    public async Task<bool> DeleteCustomerByIdAsync(string customerId)
    {
        return await _userService.DeleteUserByIdAsync(customerId);
    }

    public async Task<bool> CustomerExistsByIdAsync(string customerId, CancellationToken cancellationToken = default)
    {
        var exists = await _dbContext.Customers.AnyAsync(c => c.Id == customerId, cancellationToken: cancellationToken);
        return exists;
    }

    public async Task<bool> NationalIdExistsAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        var exists = await _dbContext.Customers.AnyAsync(c =>
                c.NationalId == nationalId, cancellationToken: cancellationToken
        );
        return exists;
    }
}