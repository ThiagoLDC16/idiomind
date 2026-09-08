namespace Idiomind.Application.Simulations.Commands.TranslateMessage;

public sealed class TranslateMessageCommandHandler(
    IAppDbContext context,
    IRequestContext requestContext,
    IMessageTranslationService messageTranslationService
) : IRequestHandler<TranslateMessageCommand, Response<TranslateMessageDto>>
{
    public async Task<Response<TranslateMessageDto>> Handle(TranslateMessageCommand request, CancellationToken cancellationToken)
    {
        var simulation = await context.Simulations
            .Include(s => s.Messages)
            .FirstOrDefaultAsync(
                s => s.Id == request.SimulationId && s.UserId == requestContext.UserId.Value,
                cancellationToken
            );

        if (simulation is null)
            return new NotFoundResponse<TranslateMessageDto>(SimulationMessages.SimulationNotFound);

        var message = simulation.Messages
            .FirstOrDefault(m => m.Id == request.MessageId && m.Sender == MessageSender.AI);

        if (message is null)
            return new NotFoundResponse<TranslateMessageDto>(SimulationMessages.MessageNotFound);

        if (message.TranslatedContent is not null)
            return new OkResponse<TranslateMessageDto>(new TranslateMessageDto(message.TranslatedContent));

        var profile = await context.UserProfiles
            .FirstOrDefaultAsync(p => p.UserId == requestContext.UserId.Value, cancellationToken);

        if (profile is null)
            return new NotFoundResponse<TranslateMessageDto>(SimulationMessages.UserProfileNotFound);

        var targetLanguage = profile.NativeLanguage;

        var result = await messageTranslationService.TranslateAsync(
            new TranslationRequest(message.Content, targetLanguage),
            cancellationToken
        );

        message.SetTranslatedContent(result.TranslatedContent);
        await context.SaveChangesAsync(cancellationToken);

        return new OkResponse<TranslateMessageDto>(new TranslateMessageDto(result.TranslatedContent));
    }
}
