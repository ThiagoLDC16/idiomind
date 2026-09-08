namespace Idiomind.Application.Simulations.Queries.ListCategories;

public sealed record ListCategoryDto
{
    public required Guid Id { get; init; }
    public required string Icon { get; init; }
    public required string Name { get; init; }
    public required string LanguageCode { get; init; }
};