using BankApp.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
namespace BankApp.Models.Requests;

public class UpdateEmployeeRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public double Salary { get; set; }
    
}
public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator(ApplicationDbContext applicationDbContext)
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
    }
}