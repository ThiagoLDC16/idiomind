namespace Idiomind.Infrastructure.Seeding;

public static class ContentId
{
    private static readonly Guid Namespace = new("6ba7b810-9dad-11d1-80b4-00c04fd430c8");

    public static Guid From(string slug) => CreateV5(Namespace, $"idiomind:{slug}");

    private static Guid CreateV5(Guid ns, string name)
    {
        var nsBytes = ns.ToByteArray();
        Swap(nsBytes, 0, 3); Swap(nsBytes, 1, 2);
        Swap(nsBytes, 4, 5); Swap(nsBytes, 6, 7);

        var hash = SHA1.HashData([.. nsBytes, .. Encoding.UTF8.GetBytes(name)]);

        hash[6] = (byte)((hash[6] & 0x0F) | 0x50);
        hash[8] = (byte)((hash[8] & 0x3F) | 0x80);

        Swap(hash, 0, 3); Swap(hash, 1, 2);
        Swap(hash, 4, 5); Swap(hash, 6, 7);

        return new Guid(hash[..16]);
    }

    private static void Swap(byte[] b, int i, int j) => (b[i], b[j]) = (b[j], b[i]);
}
