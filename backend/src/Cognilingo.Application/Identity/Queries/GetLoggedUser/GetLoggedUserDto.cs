namespace Cognilingo.Application.Identity.Queries.GetLoggedUser;

public sealed record GetLoggedUserDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required bool HasProfile { get; init; }
    public string? NativeLanguage { get; init; }
}
