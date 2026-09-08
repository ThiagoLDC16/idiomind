namespace Idiomind.Infrastructure.Seeding.Extensions;

public static class SituationVariantObjectiveExtensions
{
    public static SituationVariantObjective WithTranslation(
        this SituationVariantObjective objective,
        string languageCode,
        string name)
    {
        objective.AddTranslation(languageCode, name);
        return objective;
    }
}
