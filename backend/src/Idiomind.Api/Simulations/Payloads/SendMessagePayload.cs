namespace Idiomind.Api.Simulations.Payloads;

public sealed record SendMessagePayload(
    string Content
)
{
    public SendMessageCommand AsCommand(Guid simulationId)
        => new(
            SimulationId: simulationId,
            Content: Content
        );
}