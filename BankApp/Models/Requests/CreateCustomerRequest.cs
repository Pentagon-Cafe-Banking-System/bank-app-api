using BankApp.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class CreateCustomerRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string SecondName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string CityOfBirth { get; set; } = string.Empty;
    public string FathersName { get; set; } = string.Empty;
}

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator(ApplicationDbContext dbContext)
    {
        RuleFor(e => e.UserName)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(4)
            .WithMessage("Username must be at least 4 characters long")
            .MaximumLength(16)
            .WithMessage("Username must be at most 16 characters long")
            .MustAsync(async (username, cancellationToken) =>
                {
                    var usernameExists = await dbContext.Users
                        .AnyAsync(user => user.NormalizedUserName == username.ToUpperInvariant(),
                            cancellationToken: cancellationToken);
                    return !usernameExists;
                }
            )
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

        RuleFor(e => e.SecondName)
            .NotEmpty()
            .WithMessage("Second name is required")
            .MinimumLength(1)
            .WithMessage("Second name must be at least 1 character long")
            .MaximumLength(50)
            .WithMessage("Second name must be at most 50 characters long");

        RuleFor(e => e.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MinimumLength(1)
            .WithMessage("Last name must be at least 1 character long")
            .MaximumLength(50)
            .WithMessage("Last name must be at most 50 characters long");

        RuleFor(e => e.NationalId)
            .MustAsync(async (nationalId, cancellationToken) =>
                {
                    var nationalIdExists = await dbContext.Customers
                        .AnyAsync(customer => customer.NationalId == nationalId, cancellationToken: cancellationToken);
                    return !nationalIdExists;
                }
            )
            .WithMessage("National ID already exists")
            .MinimumLength(9)
            .WithMessage("National id must be at least 9 digits long")
            .Matches("\\d+")
            .WithMessage("National id must consist of digits only");

        RuleFor(e => e.DateOfBirth)
            .Must(dateOfBirth =>
                {
                    var timeSpan = DateTime.UtcNow - dateOfBirth;
                    var years = timeSpan.TotalDays / 365.25;
                    return years >= 18;
                }
            )
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