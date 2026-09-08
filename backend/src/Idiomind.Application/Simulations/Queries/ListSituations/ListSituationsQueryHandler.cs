namespace Idiomind.Application.Simulations.Queries.ListSituations;

public sealed class ListSituationsQueryHandler(
    IAppDbContext context
) : IRequestHandler<ListSituationsQuery, Response<ListSituationsDto>>
{
    public async Task<Response<ListSituationsDto>> Handle(
        ListSituationsQuery request,
        CancellationToken cancellationToken
    )
    {
        var languageCode = request.LanguageCode;

        var category = await context.Categories
            .AsNoTracking()
            .Where(c => c.Id == request.CategoryId)
            .Select(c => new
            {
                Name = c.Translations
                    .Where(t => t.LanguageCode == languageCode)
                    .Select(t => t.Name)
                    .FirstOrDefault()!
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (category is null)
            return new NotFoundResponse<ListSituationsDto>(SimulationMessages.CategoryNotFound);

        var situations = await context.Situations
            .AsNoTracking()
            .Where(s => s.CategoryId == request.CategoryId)
            .Select(s => new ListSituationDto
            {
                Id = s.Id,
                Name = s.Translations
                    .Where(t => t.LanguageCode == languageCode)
                    .Select(t => t.Name)
                    .FirstOrDefault()!,
                Description = s.Translations
                    .Where(t => t.LanguageCode == languageCode)
                    .Select(t => t.Description)
                    .FirstOrDefault()!,
                LanguageCode = languageCode
            })
            .ToListAsync(cancellationToken);

        return new OkResponse<ListSituationsDto>(new ListSituationsDto
        {
            Name = category.Name,
            Situations = situations
        });
    }
}
