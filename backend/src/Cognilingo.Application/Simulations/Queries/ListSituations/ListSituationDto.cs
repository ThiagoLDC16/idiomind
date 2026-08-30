namespace Cognilingo.Application.Simulations.Queries.ListSituations;

public sealed record ListSituationsDto
{
    public required string Name { get; init; }
    public required IEnumerable<ListSituationDto> Situations { get; init; }
}

public sealed record ListSituationDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string LanguageCode { get; init; }
};
