namespace Idiomind.Application.Simulations.Queries.ListCategories;

public sealed record ListCategoriesQuery(
    string LanguageCode
) : IRequest<Response<IEnumerable<ListCategoryDto>>>;