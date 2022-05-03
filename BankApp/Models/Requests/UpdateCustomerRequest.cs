using BankApp.Services.CustomerService;
using BankApp.Services.UserService;
using BankApp.Utils;
using FluentValidation;

namespace BankApp.Models.Requests;

public class UpdateCustomerRequest
{
    public string CustomerId { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string CityOfBirth { get; set; } = default!;
    public string FathersName { get; set; } = default!;
}

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator(IUserService userService, ICustomerService customerService)
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(e => e.UserName)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(4)
            .WithMessage("Username must be at least 4 characters long")
            .MaximumLength(16)
            .WithMessage("Username must be at most 16 characters long");

        RuleFor(e => new {e.CustomerId, e.UserName})
            .MustAsync(async (args, cancellationToken) =>
            {
                if (!await userService.UserNameExistsAsync(args.UserName, cancellationToken))
                    return true;
                var user = await userService.GetUserByUserNameAsync(args.UserName);
                return user.Id == args.CustomerId;
            })
            .WithName("UserName")
            .WithMessage("Username already exists");

        RuleFor(e => e.Password)
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long")
            .MaximumLength(16)
            .WithMessage("Password must be at most 16 characters long")
            .When(e => !string.IsNullOrEmpty(e.Password));

        RuleFor(e => e.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MinimumLength(1)
            .WithMessage("First name must be at least 1 character long")
            .MaximumLength(50)
            .WithMessage("First name must be at most 50 characters long");

        RuleFor(e => e.MiddleName)
            .NotEmpty()
            .WithMessage("Middle name is required")
            .MinimumLength(1)
            .WithMessage("Middle name must be at least 1 character long")
            .MaximumLength(50)
            .WithMessage("Middle name must be at most 50 characters long");

        RuleFor(e => e.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MinimumLength(1)
            .WithMessage("Last name must be at least 1 character long")
            .MaximumLength(50)
            .WithMessage("Last name must be at most 50 characters long");

        RuleFor(e => e.NationalId)
            .Length(11)
            .WithMessage("National ID must be 11 characters long")
            .Matches("\\d+")
            .WithMessage("National id must consist of digits only");

        RuleFor(e => new {e.CustomerId, e.NationalId})
            .MustAsync(async (args, cancellationToken) =>
            {
                if (!await customerService.NationalIdExistsAsync(args.NationalId, cancellationToken))
                    return true;
                var customer = await customerService.GetCustomerByNationalIdAsync(args.NationalId, cancellationToken);
                return customer.Id == args.CustomerId;
            })
            .WithName("NationalId")
            .WithMessage("NationalId already exists");

        RuleFor(e => e.DateOfBirth)
            .Must(dateOfBirth => DateUtils.GetYearsBetween(dateOfBirth, DateTime.UtcNow) >= 18)
            .WithMessage("Customer must be at least 18 years old");

        RuleFor(e => e.CityOfBirth)
            .NotEmpty()
            .WithMessage("City of birth is required")
            .MinimumLength(1)
            .WithMessage("City of birth must be at least 1 character long")
            .MaximumLength(50)
            .WithMessage("City of birth must be at most 50 characters long");

        RuleFor(e => e.FathersName)
            .NotEmpty()
            .WithMessage("Father's name is required")
            .MinimumLength(1)
            .WithMessage("Father's name must be at least 1 character long")
            .MaximumLength(50)
            .WithMessage("Father's name must be at most 50 characters long");
    }
}