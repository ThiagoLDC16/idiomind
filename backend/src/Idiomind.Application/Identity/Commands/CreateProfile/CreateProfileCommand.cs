namespace Idiomind.Application.Identity.Commands.CreateProfile;

public sealed record CreateProfileCommand(
    string NativeLanguage,
    string LearningLanguage
) : IRequest<Response>;

public sealed class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
{
    private static readonly IReadOnlySet<string> ValidCodes = new HashSet<string>
    {
        "pt", "en", "es", "fr", "de", "it", "ja", "zh", "ko"
    };

    public CreateProfileCommandValidator()
    {
        RuleFor(c => c.NativeLanguage)
            .NotEmpty()
            .Must(code => ValidCodes.Contains(code))
            .WithMessage(IdentityMessages.InvalidLanguage);

        RuleFor(c => c.LearningLanguage)
            .NotEmpty()
            .Must(code => ValidCodes.Contains(code))
            .WithMessage(IdentityMessages.InvalidLanguage);

        RuleFor(c => c)
            .Must(c => c.NativeLanguage != c.LearningLanguage)
            .WithMessage(IdentityMessages.LanguagesMustBeDifferent);
    }
}
