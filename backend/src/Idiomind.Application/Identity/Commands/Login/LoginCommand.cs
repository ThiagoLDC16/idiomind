namespace Idiomind.Application.Identity.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password
) : IRequest<Response<AuthDto>>;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(command => command.Password)
            .NotEmpty();
    }
}