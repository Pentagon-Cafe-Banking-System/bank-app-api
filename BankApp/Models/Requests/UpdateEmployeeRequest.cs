using BankApp.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class UpdateEmployeeRequest
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public double Salary { get; set; }
}

public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator(ApplicationDbContext applicationDbContext)
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(e => e.UserName).MustAsync(async (username, _) =>
            {
                var result = await applicationDbContext.Users.AnyAsync(user =>
                    user.NormalizedUserName == username.ToUpper()
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