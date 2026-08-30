namespace Cognilingo.Application.Simulations.Queries.ListSituationVariants;

public sealed class ListSituationVariantsQueryHandler(
    IAppDbContext context
) : IRequestHandler<ListSituationVariantsQuery, Response<ListSituationVariantsDto>>
{
    public async Task<Response<ListSituationVariantsDto>> Handle(
        ListSituationVariantsQuery request,
        CancellationToken cancellationToken
    )
    {
        var languageCode = request.LanguageCode;

        var situation = await context.Situations
            .AsNoTracking()
            .Where(s => s.Id == request.SituationId)
            .Select(s => new
            {
                Name = s.Translations
                    .Where(t => t.LanguageCode == languageCode)
                    .Select(t => t.Name)
                    .FirstOrDefault()!
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (situation is null)
            return new NotFoundResponse<ListSituationVariantsDto>(SimulationMessages.SituationNotFound);

        var variants = await context.SituationVariants
            .AsNoTracking()
            .Where(v => v.SituationId == request.SituationId)
            .Select(v => new ListSituationVariantDto
            {
                Id = v.Id,
                Name = v.Translations
                    .Where(t => t.LanguageCode == languageCode)
                    .Select(t => t.Name)
                    .FirstOrDefault()!,
                UserContext = v.Translations
                    .Where(t => t.LanguageCode == languageCode)
                    .Select(t => t.UserContext)
                    .FirstOrDefault()!,
                LanguageCode = languageCode,
                Objectives = v.Objectives
                    .SelectMany(o => o.Translations)
                    .Where(t => t.LanguageCode == languageCode)
                    .Select(t => t.Name)
            })
            .ToListAsync(cancellationToken);

        return new OkResponse<ListSituationVariantsDto>(new ListSituationVariantsDto
        {
            Name = situation.Name,
            Variants = variants
        });
    }
}
