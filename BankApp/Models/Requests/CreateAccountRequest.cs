using BankApp.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class CreateAccountRequest
{
    public int Balance { get; set; }
    public int TransferLimit { get; set; }
    public bool IsActive { get; set; }
    public short AccountTypeId { get; set; }
    public short CurrencyId { get; set; }
    public string CustomerId { get; set; } = string.Empty;
}

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator(ApplicationDbContext dbContext)
    {
        RuleFor(e => e.Balance)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Balance must be greater than or equal to 0");

        RuleFor(e => e.TransferLimit)
            .GreaterThan(0)
            .WithMessage("Transfer limit must be greater than 0");

        RuleFor(e => e.IsActive);

        RuleFor(e => e.AccountTypeId)
            .MustAsync(async (e, cancellationToken) =>
            {
                var accountType = await dbContext.AccountTypes
                    .FirstOrDefaultAsync(accountType => accountType.Id == e, cancellationToken);
                return accountType != null;
            })
            .WithMessage("Account type could not be found");

        RuleFor(e => e.CurrencyId)
            .MustAsync(async (e, cancellationToken) =>
            {
                var currency = await dbContext.Currencies
                    .FirstOrDefaultAsync(currency => currency.Id == e, cancellationToken);
                return currency != null;
            })
            .WithMessage("Currency could not be found");

        RuleFor(e => new {e.AccountTypeId, e.CurrencyId})
            .Must((args, _) =>
            {
                if (args.AccountTypeId is 1 or 2) return args.AccountTypeId is 1 or 2 && args.CurrencyId == 1;
                return true;
            })
            .WithName("CurrencyId")
            .WithMessage("Chosen account type only supports PLN currency")
            .Must((args, _) =>
            {
                if (args.AccountTypeId == 3) return args.AccountTypeId == 3 && args.CurrencyId != 1;
                return true;
            })
            .WithName("CurrencyId")
            .WithMessage("Foreign currency accounts don't support this currency");

        RuleFor(e => e.CustomerId)
            .MustAsync(async (customerId, cancellationToken) =>
                {
                    var result = await dbContext.Customers
                        .AnyAsync(customer => customer.Id == customerId, cancellationToken: cancellationToken);
                    return result;
                }
            )
            .WithMessage("Customer doesn't exist");
    }
}