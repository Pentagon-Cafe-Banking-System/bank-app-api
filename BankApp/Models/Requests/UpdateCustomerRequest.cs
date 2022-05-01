using BankApp.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models.Requests;

public class UpdateCustomerRequest
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string SecondName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator(ApplicationDbContext applicationDbContext)
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
    }
}