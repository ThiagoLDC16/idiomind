namespace Idiomind.Application.Simulations.Abstractions.AI.MessageTranslation;

public interface IMessageTranslationService
{
    Task<TranslationResponse> TranslateAsync(TranslationRequest request, CancellationToken cancellationToken);
}

public sealed record TranslationRequest(string Content, string TargetLanguage);
public sealed record TranslationResponse(string TranslatedContent);
