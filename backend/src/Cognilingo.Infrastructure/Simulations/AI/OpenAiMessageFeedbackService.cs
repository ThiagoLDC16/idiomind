using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cognilingo.Infrastructure.Simulations.AI;

public sealed class OpenAiMessageFeedbackService : IMessageFeedbackService
{
    private const int MaxHistoryMessages = 1;
    private readonly ChatClient _client;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public OpenAiMessageFeedbackService(IOptions<OpenAiFeedbackOptions> options)
    {
        var settings = options.Value;

        var clientOptions = new OpenAIClientOptions();

        if (!string.IsNullOrWhiteSpace(settings.BaseEndpoint))
            clientOptions.Endpoint = new Uri(settings.BaseEndpoint);

        var client = new OpenAIClient(new ApiKeyCredential(settings.ApiKey), clientOptions);
        _client = client.GetChatClient(settings.Model);
    }

    public async Task<FeedbackResponse> GenerateFeedbackAsync(FeedbackRequest request,
        CancellationToken cancellationToken)
    {
        var prompt = $"""
                      You are a helpful language learning assistant. 
                      Analyze the last message sent by the user in a conversation in {request.LanguageCode}.
                      Provide feedback on their language usage.
                      Ignore capitalization mistakes.
                      """;


        var historyToInclude = request.History
            .TakeLast(MaxHistoryMessages)
            .Select(MapToChatMessage);

        var messages = new List<ChatMessage>();

        messages.Add(new SystemChatMessage(prompt));
        messages.AddRange(historyToInclude);
        messages.Add(new UserChatMessage(request.UserMessage));

        var chatCompletionOptions = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "feedback",
                jsonSchema: BinaryData.FromObjectAsJson(new
                {
                    type = "object",
                    properties = new
                    {
                        classification =
                            new { type = "string", @enum = Enum.GetNames<MessageFeedbackClassification>() },
                        explanation = new { type = "string" },
                        correction = new { type = "string" }
                    },
                    required = new[] { "classification", "explanation", "correction" },
                    additionalProperties = false
                }, JsonOptions),
                jsonSchemaIsStrict: true
            )
        };

        var response = await _client.CompleteChatAsync(messages, chatCompletionOptions, cancellationToken);
        var content = response.Value.Content[0].Text;

        var feedback = JsonSerializer.Deserialize<FeedbackJsonDto>(content, JsonOptions)
                       ?? throw new InvalidOperationException($"Invalid feedback response: {content}");

        return new FeedbackResponse(
            feedback.Classification,
            feedback.Explanation,
            feedback.Correction
        );
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

    private sealed record FeedbackJsonDto(
        MessageFeedbackClassification Classification,
        string? Explanation,
        string? Correction
    );
}