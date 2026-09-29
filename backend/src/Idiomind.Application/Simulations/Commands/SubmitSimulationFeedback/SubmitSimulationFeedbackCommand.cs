namespace Idiomind.Application.Simulations.Commands.SubmitSimulationFeedback;

public sealed record SubmitSimulationFeedbackCommand(
    Guid SimulationId,
    int? ExperienceRating,
    SimulationFeedbackPreparedness? Preparedness,
    string? Improvement,
    bool Skipped
) : IRequest<Response>;

public sealed class SubmitSimulationFeedbackCommandValidator
    : AbstractValidator<SubmitSimulationFeedbackCommand>
{
    public SubmitSimulationFeedbackCommandValidator()
    {
        RuleFor(x => x.SimulationId)
            .NotEmpty();

        RuleFor(x => x.ExperienceRating)
            .InclusiveBetween(1, 5)
            .When(x => x.ExperienceRating.HasValue);

        RuleFor(x => x.Preparedness)
            .IsInEnum()
            .When(x => x.Preparedness.HasValue);

        RuleFor(x => x.Improvement)
            .MaximumLength(2000);

        RuleFor(x => x.ExperienceRating)
            .NotNull()
            .When(x => !x.Skipped)
            .WithMessage("experienceRatingRequired");

        RuleFor(x => x.Preparedness)
            .NotNull()
            .When(x => !x.Skipped)
            .WithMessage("preparednessRequired");
    }
}
