namespace Idiomind.Domain.Simulations.Entities;

public sealed class Situation : BaseEntity, ITranslatable<SituationTranslation>
{
    public Guid CategoryId { get; private set; }

    private readonly List<SituationTranslation> _translations = new();
    public IReadOnlyCollection<SituationTranslation> Translations => _translations;

    public Situation(Guid categoryId, List<SituationTranslation>? translations = null)
    {
        CategoryId = categoryId;
        _translations = translations ?? new();
    }

    private Situation()
    {
    }

    public void AddTranslation(string languageCode, string name, string description)
    {
        _translations.Add(new SituationTranslation(languageCode, name, description));
    }
}

public sealed class SituationTranslation : TranslationBase
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public SituationTranslation(string languageCode, string name, string description)
    {
        LanguageCode = languageCode;
        Name = name;
        Description = description;
    }

    private SituationTranslation() { }
}