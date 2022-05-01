using BankApp.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class CreateTransferRequest
{
    public decimal Amount { get; set; }
    public string ReceiverAccountNumber { get; set; } = default!;
    public string ReceiverName { get; set; } = default!;
    public string Title { get; set; } = default!;
    public long SenderAccountId { get; set; }
}

public class CreateTransferRequestValidator : AbstractValidator<CreateTransferRequest>
{
    public CreateTransferRequestValidator(ApplicationDbContext dbContext)
    {
        RuleFor(e => e.SenderAccountId)
            .Must(id =>
            {
                var account = dbContext.Accounts.FirstOrDefault(a => a.Id == id);
                return account?.IsActive == true;
            })
            .WithMessage("Selected account is deactivated");

        RuleFor(e => new {e.Amount, e.SenderAccountId})
            .MustAsync(async (args, cancellationToken) =>
                {
                    var senderAccount = await dbContext.Accounts
                        .FindAsync(new object?[] {args.SenderAccountId}, cancellationToken: cancellationToken);
                    return senderAccount != null;
                }
            )
            .WithName("SenderAccountId")
            .WithMessage("Sender's account could not be found")
            .MustAsync(async (args, cancellationToken) =>
                {
                    var senderAccount = await dbContext.Accounts
                        .FindAsync(new object?[] {args.SenderAccountId}, cancellationToken: cancellationToken);
                    return senderAccount?.Balance >= args.Amount;
                }
            )
            .WithName("Amount")
            .WithMessage("Insufficient funds")
            .MustAsync(async (args, cancellationToken) =>
                {
                    var senderAccount = await dbContext.Accounts
                        .FindAsync(new object?[] {args.SenderAccountId}, cancellationToken: cancellationToken);
                    return args.Amount <= senderAccount?.TransferLimit;
                }
            )
            .WithName("Amount")
            .WithMessage("Transfer limit exceeded");

        RuleFor(e => new {e.ReceiverAccountNumber, e.SenderAccountId})
            .MustAsync(async (args, cancellationToken) =>
            {
                var senderAccount = await dbContext.Accounts
                    .FindAsync(new object?[] {args.SenderAccountId}, cancellationToken: cancellationToken);
                return senderAccount?.Number != args.ReceiverAccountNumber;
            })
            .WithName("ReceiverAccountNumber")
            .WithMessage("Cannot send money to yourself");

        RuleFor(e => e.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");

        RuleFor(e => e.ReceiverAccountNumber)
            .MustAsync(async (accountNumber, cancellationToken) =>
                {
                    var result = await dbContext.Accounts
                        .AnyAsync(number => number.Number == accountNumber, cancellationToken: cancellationToken);
                    return result;
                }
            )
            .WithMessage("Receiver's account number does not exist")
            .MustAsync(async (accountNumber, cancellationToken) =>
                {
                    var account = await dbContext.Accounts
                        .FirstOrDefaultAsync(number =>
                                number.Number == accountNumber, cancellationToken: cancellationToken
                        );
                    return account?.IsActive == true;
                }
            )
            .WithMessage("Receiver's account is deactivated");

        RuleFor(e => e.ReceiverName)
            .NotEmpty()
            .WithMessage("Receiver's name is required")
            .MaximumLength(50)
            .WithMessage("Receiver's name must be less than 50 characters");

        RuleFor(e => e.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(50)
            .WithMessage("Title must be less than 50 characters");
    }
}