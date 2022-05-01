using BankApp.Services.CustomerService;
using BankApp.Services.UserService;
using BankApp.Utils;
using FluentValidation;

namespace BankApp.Models.Requests;

public class CreateCustomerRequest
{
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

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator(IUserService userService, ICustomerService customerService)
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(e => e.UserName)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(4)
            .WithMessage("Username must be at least 4 characters long")
            .MaximumLength(16)
            .WithMessage("Username must be at most 16 characters long")
            .MustAsync(async (userName, cancellationToken) =>
                !await userService.UserNameExistsAsync(userName, cancellationToken))
            .WithMessage("Username already exists");

        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long")
            .MaximumLength(16)
            .WithMessage("Password must be at most 16 characters long");

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
            .MustAsync(async (nationalId, cancellationToken) =>
                !await customerService.NationalIdExistsAsync(nationalId, cancellationToken))
            .WithMessage("National ID already exists")
            .MinimumLength(9)
            .WithMessage("National id must be at least 9 digits long")
            .Matches("\\d+")
            .WithMessage("National id must consist of digits only");

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