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
    public CreateEmployeeRequestValidator(ApplicationDbContext applicationDbContext)
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
        RuleFor(e => e.LastName).NotNull();
        RuleFor(e => e.Salary).GreaterThan(0);
        var genders = new List<char> {GenderType.Male, GenderType.Female};
        RuleFor(e => e.Gender).Must(g => genders.Contains(g));
        RuleFor(e => e.DateOfEmployment).GreaterThan(e => e.DateOfBirth);
        RuleFor(e => e.DateOfBirth).NotNull();
        RuleFor(e => e.DateOfEmployment).NotNull();
    }
}