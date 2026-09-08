namespace Idiomind.Application.Identity.Queries.GetLanguages;

public sealed class GetLanguagesQueryHandler
    : IRequestHandler<GetLanguagesQuery, Response<IReadOnlyList<LanguageDto>>>
{
    private static readonly IReadOnlyList<LanguageDto> Languages =
    [
        new("pt", "Português", "🇧🇷"),
        new("en", "English",   "🇺🇸"),
        new("es", "Español",   "🇪🇸"),
        new("fr", "Français",  "🇫🇷"),
        new("de", "Deutsch",   "🇩🇪"),
        new("it", "Italiano",  "🇮🇹"),
        new("ja", "日本語",     "🇯🇵"),
        new("zh", "中文",       "🇨🇳"),
        new("ko", "한국어",     "🇰🇷"),
    ];

    public Task<Response<IReadOnlyList<LanguageDto>>> Handle(
        GetLanguagesQuery request,
        CancellationToken cancellationToken)
        => Task.FromResult<Response<IReadOnlyList<LanguageDto>>>(new OkResponse<IReadOnlyList<LanguageDto>>(Languages));
}
