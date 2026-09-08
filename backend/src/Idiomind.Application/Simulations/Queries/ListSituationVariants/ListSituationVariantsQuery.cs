namespace Idiomind.Application.Simulations.Queries.ListSituationVariants;

public sealed record ListSituationVariantsQuery(
    Guid SituationId,
    string LanguageCode
) : IRequest<Response<ListSituationVariantsDto>>;
