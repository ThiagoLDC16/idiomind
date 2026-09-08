namespace Idiomind.Infrastructure.Simulations.AI;

public sealed class OpenAiChatOptions
{
    public const string SectionName = "AI:Chat";
    
    public string ApiKey { get; init; } = null!;
    public string? BaseEndpoint { get; init; }
    public string Model { get; init; } = null!;
}

public sealed class OpenAiFeedbackOptions
{
    public const string SectionName = "AI:Feedback";

    public string ApiKey { get; init; } = null!;
    public string? BaseEndpoint { get; init; }
    public string Model { get; init; } = null!;
}

public sealed class OpenAiTranslationOptions
{
    public const string SectionName = "AI:Translation";

    public string ApiKey { get; init; } = null!;
    public string? BaseEndpoint { get; init; }
    public string Model { get; init; } = null!;
}
