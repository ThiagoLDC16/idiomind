namespace Idiomind.Application.Simulations.Abstractions.AI.ChatCompletion;

public interface IChatCompletionService
{
    Task<ChatResponse> GenerateResponseAsync(ChatRequest request, CancellationToken cancellationToken);
}

public sealed record ChatRequest(
    IReadOnlyList<Message> History,
    string SystemPrompt,
    string UserMessage
);

public sealed record ChatResponse(
    string AssistantResponse
);