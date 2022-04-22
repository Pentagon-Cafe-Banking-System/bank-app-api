using BankApp.Data;
using BankApp.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class CreateTransferRequest
{
    public decimal Amount { get; set; }
    public string ReceiverAccountNumber { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long SenderAccountId { get; set; }
}

public class CreateTransferRequestValidator : AbstractValidator<CreateTransferRequest>
{
    public CreateTransferRequestValidator(ApplicationDbContext applicationDbContext)
    {
        RuleFor(e => new {e.Amount, AccountId = e.SenderAccountId}).MustAsync(async (args, _) =>
            {
                var account = await applicationDbContext.Accounts.FindAsync(args.AccountId);
                if (account == null)
                    throw new AppException("Account with requested id could not be found");
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
        ).WithMessage("Receiver's account number does not exist");
        RuleFor(e => e.ReceiverName).NotNull();
        RuleFor(e => e.Description).NotNull();
    }
}