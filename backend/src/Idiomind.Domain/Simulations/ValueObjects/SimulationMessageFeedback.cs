namespace Idiomind.Domain.Simulations.ValueObjects;

public sealed class SimulationMessageFeedback
{
    public MessageFeedbackClassification Classification { get; private set; }
    public string? Explanation { get; private set; }
    public string? Correction { get; private set; }

    private SimulationMessageFeedback()
    {
    }

    public SimulationMessageFeedback(
        MessageFeedbackClassification classification,
        string? explanation,
        string? correction
    )
    {
        Classification = classification;
        Explanation = explanation;
        Correction = correction;
    }
}