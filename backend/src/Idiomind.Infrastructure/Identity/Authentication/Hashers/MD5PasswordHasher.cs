namespace Idiomind.Infrastructure.Identity.Authentication.Hashers;

public sealed class MD5PasswordHasher : IPasswordHasher
{
    public string Hash(string plainText)
    {
        var hashedBytes = MD5.HashData(Encoding.UTF8.GetBytes(plainText));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }

    public bool Verify(string plainText, string hash)
        => hash.Equals(Hash(plainText), StringComparison.OrdinalIgnoreCase);
}