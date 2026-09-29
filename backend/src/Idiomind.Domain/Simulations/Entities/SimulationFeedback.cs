using Idiomind.Domain.Simulations.Enums;

namespace Idiomind.Domain.Simulations.Entities;

public sealed class SimulationFeedback : BaseEntity
{
    public Guid SimulationId { get; private set; }
    public Guid UserId { get; private set; }
    public Simulation? Simulation { get; private set; }
    public int? ExperienceRating { get; private set; }
    public SimulationFeedbackPreparedness? Preparedness { get; private set; }
    public string? Improvement { get; private set; }
    public bool Skipped { get; private set; }

    private SimulationFeedback()
    {
    }

    public SimulationFeedback(
        Guid simulationId,
        Guid userId,
        int? experienceRating,
        SimulationFeedbackPreparedness? preparedness,
        string? improvement,
        bool skipped
    )
    {
        SimulationId = simulationId;
        UserId = userId;
        ExperienceRating = experienceRating;
        Preparedness = preparedness;
        Improvement = improvement;
        Skipped = skipped;
    }
}
