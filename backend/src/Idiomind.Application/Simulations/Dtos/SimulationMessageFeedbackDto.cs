namespace Idiomind.Application.Simulations.Dtos;

public sealed record SimulationMessageFeedbackDto(
    MessageFeedbackClassification Classification,
    string? Explanation,
    string? Correction
);
