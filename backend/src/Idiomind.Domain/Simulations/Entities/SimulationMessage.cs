namespace Idiomind.Domain.Simulations.Entities;

public sealed class SimulationMessage : BaseEntity
{
    public Guid SimulationId { get; private set; }
    public MessageSender Sender { get; private set; }
    public string Content { get; private set; }
    public string? TranslatedContent { get; private set; }
    public SimulationMessageFeedback? Feedback { get; private set; }

    private SimulationMessage()
    {
    }

    internal SimulationMessage(
        Guid simulationId,
        MessageSender sender,
        string content,
        string? translatedContent = null,
        SimulationMessageFeedback? feedback = null
    )
    {
        SimulationId = simulationId;
        Sender = sender;
        Content = content;
        TranslatedContent = translatedContent;
        Feedback = feedback;
    }

    public void SetFeedback(SimulationMessageFeedback feedback)
        => Feedback = feedback;

    public void SetTranslatedContent(string translatedContent)
        => TranslatedContent = translatedContent;
}