using Microsoft.EntityFrameworkCore.Migrations;

namespace Idiomind.Infrastructure.Seeding.Extensions;

public static class MigrationBuilderSituationExtensions
{
    public static MigrationBuilder InsertSituation(
        this MigrationBuilder builder,
        string slug,
        Situation situation)
    {
        var id  = ContentId.From(slug);
        var now = DateTime.UtcNow;

        builder.InsertData(SeedTableNames.Situations,
            columns: ["Id", "CategoryId", "CreatedAt"],
            values:  [id, situation.CategoryId, now]);

        foreach (var t in situation.Translations)
            builder.InsertData(SeedTableNames.SituationTranslations,
                columns: ["EntityId", "LanguageCode", "Name", "Description", "Id", "CreatedAt"],
                values:  [id, t.LanguageCode, t.Name, t.Description,
                          ContentId.From($"{slug}:{t.LanguageCode}"), now]);

        return builder;
    }

    public static MigrationBuilder DeleteSituation(
        this MigrationBuilder builder,
        string slug)
    {
        builder.DeleteData(SeedTableNames.Situations, "Id", ContentId.From(slug));
        return builder;
    }
}
