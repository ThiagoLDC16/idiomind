namespace Idiomind.Application.Identity.Commands.Logout;

public sealed record LogoutCommand(
    string RefreshToken
) : IRequest<Response>;

public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
            .NotEmpty();
    }
}
