namespace Idiomind.Application.Simulations.Queries.ListCategories;

public sealed class ListCategoriesQueryHandler(
    IAppDbContext context
) : IRequestHandler<ListCategoriesQuery, Response<IEnumerable<ListCategoryDto>>>
{
    public async Task<Response<IEnumerable<ListCategoryDto>>> Handle(
        ListCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var languageCode = request.LanguageCode;
        
        var categories = await context.Categories
            .AsNoTracking()
            .Select(c => new ListCategoryDto
            {
                Id = c.Id,
                Icon = c.Icon,
                Name = c.Translations
                    .Where(t => t.LanguageCode == languageCode)
                    .Select(t => t.Name)
                    .FirstOrDefault()!,
                LanguageCode = languageCode
            })
            .ToListAsync(cancellationToken: cancellationToken);

        return new OkResponse<IEnumerable<ListCategoryDto>>(categories);
    }
}