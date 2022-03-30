using System.Data;
using BankApp.Data;
using BankApp.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Models.Requests;

public class CreateTransferRequest
{
    public decimal Amount { get; set; }
    public string ReceiverAccountNumber { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long AccountId { get; set; }
}

public class CreateTransferRequestValidator : AbstractValidator<CreateTransferRequest>
    {
        public CreateTransferRequestValidator(ApplicationDbContext applicationDbContext)
        {
            RuleFor(e => new {e.Amount, e.AccountId}).MustAsync(async (args, _) =>
                {
                    var account = await applicationDbContext.Accounts.FindAsync(args.AccountId);
                    var balance = account.Balance;
                    var transferLimit = account.TransferLimit;
                    var result = args.Amount <= balance && args.Amount <= transferLimit;
                    return result;
                }
            ).WithMessage("Amount is greater than balance or transfer limit");
            RuleFor(e => e.Amount).GreaterThan(0);
            RuleFor(e => e.ReceiverAccountNumber).MustAsync(async (numberAccount, _) =>
                    {
                        var result = await applicationDbContext.Accounts.AnyAsync(number =>
                            number.Number == numberAccount
                        );
                        return result;
                    }
                ).WithMessage("Number doesn't exist)");
            RuleFor(e => e.ReceiverName).NotNull();
            RuleFor(e => e.Description).NotNull();
        }
    }