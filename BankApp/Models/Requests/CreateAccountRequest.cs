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
    public short AccountTypeId { get; set; }
    public short CurrencyId { get; set; }
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

        RuleFor(e => e.AccountTypeId)
            .MustAsync(async (e, _) => await accountTypeService.AccountTypeExistsByIdAsync(e))
            .WithMessage("Account type does not exist");

        RuleFor(e => e.CurrencyId)
            .MustAsync(async (e, _) => await currencyService.CurrencyExistsByIdAsync(e))
            .WithMessage("Currency does not exist");

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
            .MustAsync(async (customerId, _) => await customerService.CustomerExistsByIdAsync(customerId))
            .WithMessage("Customer doesn't exist");
    }
}