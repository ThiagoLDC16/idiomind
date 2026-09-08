using Microsoft.EntityFrameworkCore.Migrations;

namespace Idiomind.Infrastructure.Seeding.Extensions;

public static class MigrationBuilderCategoryExtensions
{
    public static MigrationBuilder InsertCategory(
        this MigrationBuilder builder,
        string slug,
        Category category)
    {
        var id  = ContentId.From(slug);
        var now = DateTime.UtcNow;

        builder.InsertData(SeedTableNames.Categories,
            columns: ["Id", "Icon", "CreatedAt"],
            values:  [id, category.Icon, now]);

        foreach (var t in category.Translations)
            builder.InsertData(SeedTableNames.CategoryTranslations,
                columns: ["EntityId", "LanguageCode", "Name", "Id", "CreatedAt"],
                values:  [id, t.LanguageCode, t.Name,
                          ContentId.From($"{slug}:{t.LanguageCode}"), now]);

        return builder;
    }

    public static MigrationBuilder DeleteCategory(
        this MigrationBuilder builder,
        string slug)
    {
        builder.DeleteData(SeedTableNames.Categories, "Id", ContentId.From(slug));
        return builder;
    }
}
