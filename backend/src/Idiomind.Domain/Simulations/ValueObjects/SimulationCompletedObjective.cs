namespace Idiomind.Domain.Simulations.ValueObjects;

public sealed record SimulationCompletedObjective
{
    public required Guid SimulationId { get; init; }
    public required Guid ObjectiveId { get; init; }
    public required DateTime? CompletedAt { get; init; }
}