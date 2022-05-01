using BankApp.Services.UserService;
using FluentValidation;

namespace BankApp.Models.Requests;

public class LoginRequest
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator(IUserService userService)
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(e => e.UserName)
            .MustAsync(async (userName, cancellationToken) =>
                await userService.UserNameExistsAsync(userName, cancellationToken))
            .WithMessage("Username does not exist");

        RuleFor(e => new {e.UserName, e.Password})
            .MustAsync(async (args, _) =>
                await userService.ValidateUserPasswordAsync(args.UserName, args.Password))
            .WithName("Password")
            .WithMessage("Password is incorrect");
    }
}