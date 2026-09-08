namespace Idiomind.Application.Identity.Commands.RefreshTokens;

public sealed record RefreshTokenCommand(
    string RefreshToken
) : IRequest<Response<AuthDto>>;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
            .NotEmpty();
    }
}
