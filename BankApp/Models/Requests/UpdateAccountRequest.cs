using BankApp.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class UpdateAccountRequest
{
    public int Balance { get; set; }
    public int TransferLimit { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateAccountRequestValidator : AbstractValidator<UpdateAccountRequest>
{
    public UpdateAccountRequestValidator(ApplicationDbContext applicationDbContext)
    {
        RuleFor(e => e.Balance).GreaterThanOrEqualTo(0);
        RuleFor(e => e.TransferLimit).GreaterThan(0);
        RuleFor(e => e.IsActive).NotNull();
    }
}