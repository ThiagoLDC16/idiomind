namespace Cognilingo.Application.Simulations.Queries.ListSimulationMessages;

public sealed record ListSimulationMessagesDto
{
    public required string Name { get; init; }
    public required string LearningLanguage { get; init; }
    public required IEnumerable<ListSimulationMessageDto> Messages { get; init; }
}

public sealed record ListSimulationMessageDto
{
    public required Guid Id { get; init; }
    public required MessageSender Sender { get; init; }
    public required string Content { get; init; }
    public string? TranslatedContent { get; init; }
    public ListSimulationMessageFeedbackDto? Feedback { get; init; }
};

public sealed record ListSimulationMessageFeedbackDto
{
    public required MessageFeedbackClassification Classification { get; init; }
    public string? Explanation { get; init; }
    public string? Correction { get; init; }
}
