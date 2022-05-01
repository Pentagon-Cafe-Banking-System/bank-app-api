using BankApp.Services.AccountTypeService;
using BankApp.Services.CurrencyService;
using BankApp.Services.CustomerService;
using FluentValidation;

namespace BankApp.Models.Requests;

public class CreateAccountRequest
{
    public int Balance { get; set; }
    public int TransferLimit { get; set; }
    public bool IsActive { get; set; }
    public int AccountTypeId { get; set; }
    public int CurrencyId { get; set; }
    public string CustomerId { get; set; } = default!;
}

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator(
        IAccountTypeService accountTypeService,
        ICurrencyService currencyService,
        ICustomerService customerService)
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(e => e.Balance)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Balance must be greater than or equal to 0");

        RuleFor(e => e.TransferLimit)
            .GreaterThan(0)
            .WithMessage("Transfer limit must be greater than 0");

        RuleFor(e => e.IsActive);

        RuleFor(e => e.CustomerId)
            .MustAsync(async (customerId, cancellationToken) =>
                await customerService.CustomerExistsByIdAsync(customerId, cancellationToken))
            .WithMessage("Customer doesn't exist");

        RuleFor(e => e.AccountTypeId)
            .MustAsync(async (accountTypeId, cancellationToken) =>
                await accountTypeService.AccountTypeExistsByIdAsync(accountTypeId, cancellationToken))
            .WithMessage("Account type does not exist");

        RuleFor(e => e.CurrencyId)
            .MustAsync(async (e, cancellationToken) =>
                await currencyService.CurrencyExistsByIdAsync(e, cancellationToken))
            .WithMessage("Currency does not exist");

        RuleFor(e => new {e.AccountTypeId, e.CurrencyId})
            .MustAsync(async (args, cancellationToken) => await accountTypeService
                .AccountTypeSupportsCurrencyAsync(args.AccountTypeId, args.CurrencyId, cancellationToken))
            .WithName("CurrencyId")
            .WithMessage("Chosen account does not support this currency");
    }
}