namespace Idiomind.Domain.Identity.Entities;

public sealed class RefreshToken : BaseEntity
{
    public const int DaysToExpire = 30;

    public Guid UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }

    public User User { get; set; }

    public RefreshToken(Guid userId, string token)
    {
        UserId = userId;
        UpdateToken(token);
    }

    public void UpdateToken(string token)
    {
        Token = token;
        ExpiresAt = DateTime.UtcNow.AddDays(DaysToExpire);
    }

    public bool IsExpired() => ExpiresAt < DateTime.UtcNow;
}