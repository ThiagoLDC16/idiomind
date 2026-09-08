namespace Idiomind.Application.Simulations.Queries.ListSimulationMessages;

public sealed class ListSimulationMessagesQueryHandler(
    IAppDbContext context
) : IRequestHandler<ListSimulationMessagesQuery, Response<ListSimulationMessagesDto>>
{
    public async Task<Response<ListSimulationMessagesDto>> Handle(
        ListSimulationMessagesQuery request,
        CancellationToken cancellationToken
    )
    {
        var simulation = await context.Simulations
            .AsNoTracking()
            .Where(s => s.Id == request.SimulationId)
            .Select(s => new ListSimulationMessagesDto
            {
                SituationId = s.SituationId,
                CategoryId = context.Situations
                    .Where(situation => situation.Id == s.SituationId)
                    .Select(situation => situation.CategoryId)
                    .First(),
                Name = s.Variant.Translations
                    .Where(t => t.LanguageCode == request.LanguageCode)
                    .Select(t => t.Name)
                    .FirstOrDefault()!,
                LearningLanguage = s.Variant.LearningLanguage,
                Messages = s.Messages
                    .OrderBy(m => m.CreatedAt)
                    .Select(m => new ListSimulationMessageDto
                    {
                        Id = m.Id,
                        Sender = m.Sender,
                        Content = m.Content,
                        TranslatedContent = m.TranslatedContent,
                        Feedback = m.Feedback != null
                            ? new ListSimulationMessageFeedbackDto
                            {
                                Classification = m.Feedback.Classification,
                                Explanation = m.Feedback.Explanation,
                                Correction = m.Feedback.Correction
                            }
                            : null
                    })
            })
            .FirstOrDefaultAsync(cancellationToken);

        return simulation is null
            ? new NotFoundResponse<ListSimulationMessagesDto>(SimulationMessages.SimulationNotFound)
            : new OkResponse<ListSimulationMessagesDto>(simulation);
    }
}
