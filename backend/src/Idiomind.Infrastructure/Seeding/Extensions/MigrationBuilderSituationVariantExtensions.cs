using Microsoft.EntityFrameworkCore.Migrations;

namespace Idiomind.Infrastructure.Seeding.Extensions;

public static class MigrationBuilderSituationVariantExtensions
{
    public static MigrationBuilder InsertSituationVariant(
        this MigrationBuilder builder,
        string slug,
        SituationVariant variant)
    {
        var id  = ContentId.From(slug);
        var now = DateTime.UtcNow;

        builder.InsertData(SeedTableNames.SituationVariants,
            columns: ["Id", "SituationId", "LearningLanguage",
                      "PromptInstructions", "InitialMessage",
                      "SituationVariantBaseId", "CreatedAt"],
            values:  [id, variant.SituationId, variant.LearningLanguage,
                      variant.PromptInstructions, variant.InitialMessage,
                      id,
                      now]);

        foreach (var t in variant.Translations)
            builder.InsertData(SeedTableNames.SituationVariantTranslations,
                columns: ["EntityId", "LanguageCode", "Name", "UserContext", "Id", "CreatedAt"],
                values:  [id, t.LanguageCode, t.Name, t.UserContext,
                          ContentId.From($"{slug}:{t.LanguageCode}"), now]);

        return builder;
    }

    public static MigrationBuilder DeleteSituationVariant(
        this MigrationBuilder builder,
        string slug)
    {
        builder.DeleteData(SeedTableNames.SituationVariants, "Id", ContentId.From(slug));
        return builder;
    }

    public static MigrationBuilder InsertObjective(
        this MigrationBuilder builder,
        string slug,
        SituationVariantObjective objective)
    {
        var id  = ContentId.From(slug);
        var now = DateTime.UtcNow;

        builder.InsertData(SeedTableNames.SituationVariantObjectives,
            columns: ["Id", "SituationVariantId", "CreatedAt"],
            values:  [id, objective.SituationVariantId, now]);

        foreach (var t in objective.Translations)
            builder.InsertData(SeedTableNames.SituationVariantObjectiveTranslations,
                columns: ["EntityId", "LanguageCode", "Name", "Id", "CreatedAt"],
                values:  [id, t.LanguageCode, t.Name,
                          ContentId.From($"{slug}:{t.LanguageCode}"), now]);

        return builder;
    }

    public static MigrationBuilder DeleteObjective(
        this MigrationBuilder builder,
        string slug)
    {
        builder.DeleteData(SeedTableNames.SituationVariantObjectives, "Id", ContentId.From(slug));
        return builder;
    }
}
