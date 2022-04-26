using BankApp.Entities.UserTypes;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Models.Requests;

public class LoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator(UserManager<AppUser> userManager)
    {
        RuleFor(e => e.UserName)
            .MustAsync(async (userName, _) =>
            {
                var user = await userManager.FindByNameAsync(userName);
                return user != null;
            })
            .WithMessage("Username does not exist");

        RuleFor(e => new {e.UserName, e.Password})
            .MustAsync(async (args, _) =>
            {
                var user = await userManager.FindByNameAsync(args.UserName);
                var isPasswordCorrect = await userManager.CheckPasswordAsync(user, args.Password);
                return isPasswordCorrect;
            })
            .WithName("Password")
            .WithMessage("Password is incorrect");
    }
}