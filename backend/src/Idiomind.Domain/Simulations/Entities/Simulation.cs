namespace Idiomind.Domain.Simulations.Entities;

public sealed class Simulation : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid SituationId { get; private set; }
    public Guid VariantId { get; private set; }
    public SituationVariant Variant { get; private set; } = null!;
    public SimulationStatus Status { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private readonly List<SimulationMessage> _messages = new();
    public IReadOnlyCollection<SimulationMessage> Messages => _messages;

    private readonly List<SimulationCompletedObjective> _completedObjectives = new();
    public IReadOnlyCollection<SimulationCompletedObjective> CompletedObjectives => _completedObjectives;

    private Simulation()
    {
    }

    public Simulation(
        Guid userId,
        Guid situationId,
        Guid variantId,
        string initialMessage
    )
    {
        UserId = userId;
        SituationId = situationId;
        VariantId = variantId;
        Status = SimulationStatus.InProgress;

        AddAssistantMessage(initialMessage);
    }

    public SimulationMessage AddUserMessage(string content)
    {
        EnsureInProgress();

        var message = new SimulationMessage(
            simulationId: Id,
            sender: MessageSender.User,
            content: content
        );
        _messages.Add(message);

        return message;
    }

    public SimulationMessage AddAssistantMessage(string content)
    {
        EnsureInProgress();

        var message = new SimulationMessage(
            simulationId: Id,
            sender: MessageSender.AI,
            content: content
        );
        _messages.Add(message);

        return message;
    }

    public void CompleteObjective(Guid objectiveId)
    {
        EnsureInProgress();

        if (_completedObjectives.Any(o => o.ObjectiveId == objectiveId))
            return;

        _completedObjectives.Add(new SimulationCompletedObjective
        {
            SimulationId = Id,
            ObjectiveId = objectiveId,
            CompletedAt = DateTime.UtcNow
        });
    }

    public void Complete()
    {
        EnsureInProgress();

        Status = SimulationStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void Abandon()
    {
        EnsureInProgress();

        Status = SimulationStatus.Abandoned;
    }

    private void EnsureInProgress()
    {
        if (Status != SimulationStatus.InProgress)
            throw new InvalidOperationException($"Simulation is not in progress. Current status: {Status}");
    }
}