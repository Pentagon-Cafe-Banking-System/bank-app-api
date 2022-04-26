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
        CascadeMode = CascadeMode.Stop;

        RuleFor(e => e.UserName)
            .NotEmpty()
            .WithMessage("Username is required")
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
            .WithMessage("Password is required");

        RuleFor(e => e.FirstName)
            .NotEmpty()
            .WithMessage("First name is required");

        RuleFor(e => e.LastName)
            .NotEmpty()
            .WithMessage("Last name is required");

        RuleFor(e => e.Salary)
            .GreaterThan(0)
            .WithMessage("Salary must be greater than 0");

        var genders = new List<char> {GenderType.Male, GenderType.Female};
        RuleFor(e => e.Gender)
            .Must(g => genders.Contains(g));

        RuleFor(e => e.DateOfEmployment)
            .GreaterThan(e => e.DateOfBirth)
            .WithMessage("Date of employment cannot be before than date of birth");

        RuleFor(e => e.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of birth is required");

        RuleFor(e => e.DateOfEmployment)
            .NotEmpty()
            .WithMessage("Date of employment is required");
    }
}