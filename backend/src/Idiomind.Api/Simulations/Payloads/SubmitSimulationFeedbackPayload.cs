namespace Idiomind.Api.Simulations.Payloads;

public sealed record SubmitSimulationFeedbackPayload(
    int? ExperienceRating,
    SimulationFeedbackPreparedness? Preparedness,
    string? Improvement,
    bool Skipped
)
{
    public SubmitSimulationFeedbackCommand AsCommand(Guid simulationId)
        => new(
            simulationId,
            ExperienceRating,
            Preparedness,
            Improvement,
            Skipped
        );
}
