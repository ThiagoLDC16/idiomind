namespace Idiomind.Infrastructure.Simulations.AI;

public sealed class OpenAiChatCompletionService : IChatCompletionService
{
    private const int MaxHistoryMessages = 3;
    private readonly ChatClient _client;

    public OpenAiChatCompletionService(IOptions<OpenAiChatOptions> options)
    {
        var settings = options.Value;

        var clientOptions = new OpenAIClientOptions();

        if (!string.IsNullOrWhiteSpace(settings.BaseEndpoint))
            clientOptions.Endpoint = new Uri(settings.BaseEndpoint);

        var client = new OpenAIClient(new ApiKeyCredential(settings.ApiKey), clientOptions);
        _client = client.GetChatClient(settings.Model);
    }

    public async Task<ChatResponse> GenerateResponseAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var historyToInclude = request.History
            .TakeLast(MaxHistoryMessages)
            .Select(MapToChatMessage);

        var messages = new List<ChatMessage>();
        
        var systemPrompt = $"""
                            {request.SystemPrompt}
                      
                            # Instructions:
                            - NEVER respond in markdown, ALWAYS in plain text
                            - Respond in a natural and humanized way, with one idea at a time
                            """;

        messages.Add(new SystemChatMessage(systemPrompt));
        messages.AddRange(historyToInclude);
        messages.Add(new UserChatMessage(request.UserMessage));

        var response = await _client.CompleteChatAsync(messages, cancellationToken: cancellationToken);

        return new ChatResponse(response.Value.Content[0].Text);
    }

    private static ChatMessage MapToChatMessage(Message message)
    {
        return message.Sender switch
        {
            MessageSender.AI => new AssistantChatMessage(message.Content),
            MessageSender.User => new UserChatMessage(message.Content),
            _ => throw new ArgumentOutOfRangeException(nameof(message.Sender), message.Sender, null)
        };
    }
}