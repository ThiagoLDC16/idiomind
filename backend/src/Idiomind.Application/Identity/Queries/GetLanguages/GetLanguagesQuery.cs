namespace Idiomind.Application.Identity.Queries.GetLanguages;

public sealed record GetLanguagesQuery() : IRequest<Response<IReadOnlyList<LanguageDto>>>;
