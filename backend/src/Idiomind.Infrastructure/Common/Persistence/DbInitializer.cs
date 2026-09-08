namespace Idiomind.Infrastructure.Common.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedUsersAsync(context);
    }

    private static async Task SeedUsersAsync(ApplicationDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var hasher = new MD5PasswordHasher();
        var user = new User(
            "Admin",
            "admin@teste.com",
            hasher.Hash("admin123")
        );

        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

}
