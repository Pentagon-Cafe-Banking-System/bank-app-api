using FluentValidation;

namespace BankApp.Models.Requests;

public class LoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(e => e.UserName)
            .NotEmpty()
            .WithMessage("Username is required");

        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}