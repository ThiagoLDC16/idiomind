namespace Idiomind.Infrastructure.Identity.Options;

public sealed class JwtAuthOptions
{
    public string SecurityKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}