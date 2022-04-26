using FluentValidation;

namespace BankApp.Models.Requests;

public class UpdateAccountRequest
{
    public decimal? Balance { get; set; }
    public decimal? TransferLimit { get; set; }
    public bool? IsActive { get; set; }
}

public class UpdateAccountRequestValidator : AbstractValidator<UpdateAccountRequest>
{
    public UpdateAccountRequestValidator()
    {
        RuleFor(e => e.Balance)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Balance must be greater than or equal to 0");

        RuleFor(e => e.TransferLimit)
            .GreaterThan(0)
            .WithMessage("Transfer limit must be greater than 0");

        RuleFor(e => e.IsActive);
    }
}