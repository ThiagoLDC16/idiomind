namespace Idiomind.Domain.Simulations.Entities;

public sealed class SituationVariantObjective : BaseEntity, ITranslatable<SituationVariantObjectiveTranslation>
{
    public Guid SituationVariantId { get; private set; }

    private readonly List<SituationVariantObjectiveTranslation> _translations = new();
    public IReadOnlyCollection<SituationVariantObjectiveTranslation> Translations => _translations;

    public SituationVariantObjective(Guid situationVariantId, List<SituationVariantObjectiveTranslation>? translations = null)
    {
        SituationVariantId = situationVariantId;
        _translations = translations ?? new();
    }

    private SituationVariantObjective()
    {
    }

    public void AddTranslation(string languageCode, string name)
    {
        _translations.Add(new SituationVariantObjectiveTranslation(languageCode, name));
    }
}

public sealed class SituationVariantObjectiveTranslation : TranslationBase
{
    public string Name { get; private set; }

    public SituationVariantObjectiveTranslation(string languageCode, string name)
    {
        LanguageCode = languageCode;
        Name = name;
    }

    private SituationVariantObjectiveTranslation() { }
}