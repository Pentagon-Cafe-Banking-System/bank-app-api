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
    public CreateCustomerRequestValidator(ApplicationDbContext applicationDbContext)
    {
        RuleFor(e => e.UserName).MustAsync(async (username, _) =>
            {
                var result = await applicationDbContext.Users.AnyAsync(user =>
                    user.NormalizedUserName == username.ToUpperInvariant()
                );
                return !result;
            }
        ).WithMessage("Username already exists");
        RuleFor(e => e.Password).NotNull();
        RuleFor(e => e.FirstName).NotNull();
        RuleFor(e => e.SecondName).NotNull();
        RuleFor(e => e.LastName).NotNull();
        RuleFor(e => e.NationalId).Matches("\\d+");
        RuleFor(e => e.DateOfBirth).GreaterThan(DateTime.Now.AddYears(-100));
        RuleFor(e => e.CityOfBirth).NotNull();
        RuleFor(e => e.FathersName).NotNull();
    }
}