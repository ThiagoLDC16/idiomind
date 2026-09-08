namespace Idiomind.Application.Simulations.Dtos;

public sealed record SimulationMessageDto(
    Guid Id,
    MessageSender Sender,
    string Content,
    string? TranslatedContent,
    SimulationMessageFeedbackDto? Feedback
)
{
    public static SimulationMessageDto FromDomain(SimulationMessage message)
    {
        return new SimulationMessageDto(
            message.Id,
            message.Sender,
            message.Content,
            message.TranslatedContent,
            message.Feedback is null
                ? null
                : new SimulationMessageFeedbackDto(
                    message.Feedback.Classification,
                    message.Feedback.Explanation,
                    message.Feedback.Correction
                )
        );
    }
}
