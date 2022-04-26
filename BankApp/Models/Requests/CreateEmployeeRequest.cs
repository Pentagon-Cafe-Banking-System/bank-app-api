using BankApp.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class CreateEmployeeRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public double Salary { get; set; }
    public char Gender { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator(ApplicationDbContext dbContext)
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
                    var usernameExists = await dbContext.Users.AnyAsync(
                        user => user.NormalizedUserName == username.ToUpperInvariant(),
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

        RuleFor(e => e.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MinimumLength(1)
            .WithMessage("Last name must be at least 1 character long")
            .MaximumLength(50)
            .WithMessage("Last name must be at most 50 characters long");

        RuleFor(e => e.Salary)
            .GreaterThan(0)
            .WithMessage("Salary must be greater than 0");

        var genders = new List<char> {GenderType.Male, GenderType.Female};
        RuleFor(e => e.Gender)
            .Must(g => genders.Contains(g));

        RuleFor(e => e.DateOfBirth)
            .Must(dateOfBirth =>
                {
                    var timeSpan = DateTime.UtcNow - dateOfBirth;
                    var years = timeSpan.TotalDays / 365.25;
                    return years >= 18;
                }
            )
            .WithMessage("Employee must be at least 18 years old");

        RuleFor(e => e.DateOfEmployment)
            .GreaterThan(e => e.DateOfBirth)
            .WithMessage("Date of employment cannot be before than date of birth");
    }
}