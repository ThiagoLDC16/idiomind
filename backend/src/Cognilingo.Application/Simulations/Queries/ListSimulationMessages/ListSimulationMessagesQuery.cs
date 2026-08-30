namespace Cognilingo.Application.Simulations.Queries.ListSimulationMessages;

public sealed record ListSimulationMessagesQuery(
    Guid SimulationId,
    string LanguageCode
) : IRequest<Response<ListSimulationMessagesDto>>;
