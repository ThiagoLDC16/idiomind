namespace Idiomind.Application.Simulations.Queries.ListSituationVariants;

public sealed record ListSituationVariantsDto
{
    public required string Name { get; init; }
    public required IEnumerable<ListSituationVariantDto> Variants { get; init; }
}

public sealed record ListSituationVariantDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string UserContext { get; init; }
    public required string LanguageCode { get; init; }
    public required IEnumerable<string> Objectives { get; init; }
};
