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
            .WithMessage("Password is required");

        RuleFor(e => e.FirstName)
            .NotEmpty()
            .WithMessage("First name is required");

        RuleFor(e => e.SecondName)
            .NotEmpty()
            .WithMessage("Second name is required");

        RuleFor(e => e.LastName)
            .NotEmpty()
            .WithMessage("Last name is required");

        RuleFor(e => e.NationalId)
            .Matches("\\d+")
            .WithMessage("National id must consist of digits only");

        RuleFor(e => e.DateOfBirth)
            .LessThan(DateTime.UtcNow)
            .WithMessage("Date of birth must be in the past");

        RuleFor(e => e.CityOfBirth)
            .NotEmpty()
            .WithMessage("City of birth is required");

        RuleFor(e => e.FathersName)
            .NotEmpty()
            .WithMessage("Father's name is required");
    }
}