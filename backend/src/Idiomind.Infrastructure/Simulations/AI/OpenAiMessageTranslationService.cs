using Idiomind.Application.Simulations.Abstractions.AI.MessageTranslation;

namespace Idiomind.Infrastructure.Simulations.AI;

public sealed class OpenAiMessageTranslationService : IMessageTranslationService
{
    private readonly ChatClient _client;

    public OpenAiMessageTranslationService(IOptions<OpenAiTranslationOptions> options)
    {
        var settings = options.Value;

        var clientOptions = new OpenAIClientOptions();

        if (!string.IsNullOrWhiteSpace(settings.BaseEndpoint))
            clientOptions.Endpoint = new Uri(settings.BaseEndpoint);

        var client = new OpenAIClient(new ApiKeyCredential(settings.ApiKey), clientOptions);
        _client = client.GetChatClient(settings.Model);
    }

    public async Task<TranslationResponse> TranslateAsync(TranslationRequest request, CancellationToken cancellationToken)
    {
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                $"Translate the following text to {request.TargetLanguage}. Return only the translated text, with no explanation."
            ),
            new UserChatMessage(request.Content)
        };

        var response = await _client.CompleteChatAsync(messages, cancellationToken: cancellationToken);
        var translatedContent = response.Value.Content[0].Text;

        return new TranslationResponse(translatedContent);
    }
}
