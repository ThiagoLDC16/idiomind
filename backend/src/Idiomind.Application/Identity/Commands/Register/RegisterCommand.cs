namespace Idiomind.Application.Identity.Commands.Register;

public sealed record RegisterCommand(
    string Name,
    string Email,
    string Password
) : IRequest<Response<AuthDto>>;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(command => command.Email)
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(command => command.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}