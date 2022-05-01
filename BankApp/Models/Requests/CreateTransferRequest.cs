using BankApp.Services.AccountService;
using FluentValidation;

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
    public CreateTransferRequestValidator(IAccountService accountService)
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(e => e.SenderAccountId)
            .MustAsync(async (accountId, cancellationToken) =>
                await accountService.AccountExistsByIdAsync(accountId, cancellationToken))
            .WithMessage("Sender's account does not exist")
            .MustAsync(async (accountId, cancellationToken) =>
                await accountService.IsAccountActiveByIdAsync(accountId, cancellationToken))
            .WithMessage("Selected account is deactivated");

        RuleFor(e => e.ReceiverAccountNumber)
            .MustAsync(async (accountNumber, cancellationToken) =>
                await accountService.AccountExistsByNumberAsync(accountNumber, cancellationToken))
            .WithMessage("Receiver's account number does not exist")
            .MustAsync(async (accountNumber, cancellationToken) =>
                await accountService.IsAccountActiveByNumberAsync(accountNumber, cancellationToken))
            .WithMessage("Receiver's account is deactivated");

        RuleFor(e => new {e.Amount, e.SenderAccountId})
            .MustAsync(async (args, cancellationToken) =>
                await accountService.HasSufficientFundsAsync(args.SenderAccountId, args.Amount, cancellationToken))
            .WithName("Amount")
            .WithMessage("Insufficient funds")
            .MustAsync(async (args, cancellationToken) =>
                await accountService.IsWithinTransferLimitAsync(args.SenderAccountId, args.Amount, cancellationToken))
            .WithName("Amount")
            .WithMessage("Transfer limit exceeded");

        RuleFor(e => new {e.ReceiverAccountNumber, e.SenderAccountId})
            .MustAsync(async (args, cancellationToken) =>
            {
                var senderAccount = await accountService.GetAccountByIdAsync(args.SenderAccountId, cancellationToken);
                return senderAccount.Number != args.ReceiverAccountNumber;
            })
            .WithName("ReceiverAccountNumber")
            .WithMessage("Cannot send money to yourself");

        RuleFor(e => e.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");

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