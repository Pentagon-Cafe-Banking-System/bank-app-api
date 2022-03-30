using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Services.AccountService;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class CreateAccountRequest
{
    public string Number { get; set; } = string.Empty;
    public int Balance { get; set; }
    public int TransferLimit { get; set; }
    public bool IsActive { get; set; }
    public short AccountTypeId { get; set; }
    public short CurrencyId { get; set; }
    public string CustomerId { get; set; } = string.Empty;
}

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator(ApplicationDbContext applicationDbContext)
    {
        RuleFor(e => e.Number).MustAsync(async (numberAccount, _) =>
        {
            var result = await applicationDbContext.Accounts.AnyAsync(number =>
                number.Number == numberAccount
            );
            return !result;
        }
            ).WithMessage("Number already exists");
        RuleFor(e => e.Balance).GreaterThanOrEqualTo(0);
        RuleFor(e => e.TransferLimit).GreaterThan(0);
        RuleFor(e => e.IsActive).NotNull();
        RuleFor(e => e.AccountTypeId).GreaterThanOrEqualTo((short)1).LessThanOrEqualTo((short)3);
        RuleFor(e => e.CurrencyId).GreaterThanOrEqualTo((short)1).LessThanOrEqualTo((short)14);
        RuleFor(e => e.CustomerId).MustAsync(async (customer, _) =>
            {
                var result = await applicationDbContext.Customers.AnyAsync(customerId =>
                    customerId.Id != customer
                );
                return !result;
            }
        ).WithMessage("Customer doesn't exist");
    }
}