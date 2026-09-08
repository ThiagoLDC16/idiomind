namespace Idiomind.Domain.Identity.Entities;

public sealed class User : AggregateRoot
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; }

    public List<RefreshToken> RefreshTokens { get; private set; } = new();

    private User()
    {
    }

    public User(
        string name,
        string email,
        string passwordHash
    )
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }
}