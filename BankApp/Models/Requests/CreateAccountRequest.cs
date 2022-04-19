using BankApp.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class CreateAccountRequest
{
    public int Balance { get; set; }
    public int TransferLimit { get; set; }
    public short AccountTypeId { get; set; }
    public short CurrencyId { get; set; }
    public string CustomerId { get; set; } = string.Empty;
}

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator(ApplicationDbContext applicationDbContext)
    {
        RuleFor(e => e.Balance).GreaterThanOrEqualTo(0);
        RuleFor(e => e.TransferLimit).GreaterThan(0);
        RuleFor(e => e.AccountTypeId).MustAsync(async (e, cancellationToken) =>
        {
            var accountType =
                await applicationDbContext.AccountTypes.FirstOrDefaultAsync(accountType => accountType.Id == e,
                    cancellationToken);
            return accountType != null;
        }).WithMessage("Account type not found");
        RuleFor(e => e.CurrencyId).MustAsync(async (e, cancellationToken) =>
        {
            var currency =
                await applicationDbContext.Currencies.FirstOrDefaultAsync(currency => currency.Id == e,
                    cancellationToken);
            return currency != null;
        }).WithMessage("Currency not found");        
        RuleFor(e => new {e.AccountTypeId, e.CurrencyId}).Must((args, _) =>
            {
                var result = 
                    (args.AccountTypeId is 1 or 2 && args.CurrencyId == 1) || 
                    (args.AccountTypeId == 3 && args.CurrencyId != 1);
                return result;
            }
        ).WithMessage("Current and savings accounts must be in PLN currency. Foreign currency accounts must be in foreign currency");
        RuleFor(e => e.CustomerId).MustAsync(async (customerId, _) =>
            {
                var result = await applicationDbContext.Customers.AnyAsync(customer =>
                    customer.Id == customerId
                );
                return result;
            }
        ).WithMessage("Customer doesn't exist");
    }
}