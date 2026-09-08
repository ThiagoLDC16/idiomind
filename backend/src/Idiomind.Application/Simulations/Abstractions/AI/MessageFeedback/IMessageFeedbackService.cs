namespace Idiomind.Application.Simulations.Abstractions.AI.MessageFeedback;

public interface IMessageFeedbackService
{
    Task<FeedbackResponse> GenerateFeedbackAsync(FeedbackRequest request, CancellationToken cancellationToken);
}

public sealed record FeedbackRequest(
    IReadOnlyList<Message> History,
    string LanguageCode,
    string UserMessage
);

public sealed record FeedbackResponse(
    MessageFeedbackClassification Classification,
    string? Explanation,
    string? Correction
);