namespace Idiomind.Domain.Simulations.Entities;

public sealed class SituationVariant : BaseEntity, ITranslatable<SituationVariantTranslation>
{
    public Guid SituationId { get; private set; }
    public string LearningLanguage { get; private set; }
    public string PromptInstructions { get; private set; }
    public string InitialMessage { get; private set; }
    public Guid? SituationVariantBaseId { get; private set; }

    private readonly List<SituationVariantTranslation> _translations = new();
    public IReadOnlyCollection<SituationVariantTranslation> Translations => _translations;

    private readonly List<SituationVariantObjective> _objectives = new();
    public IReadOnlyCollection<SituationVariantObjective> Objectives => _objectives;

    public SituationVariant(Guid situationId, string learningLanguage, string promptInstructions, string initialMessage, List<SituationVariantTranslation>? translations = null)
    {
        SituationId = situationId;
        LearningLanguage = learningLanguage;
        PromptInstructions = promptInstructions;
        InitialMessage = initialMessage;
        _translations = translations ?? new();
    }

    private SituationVariant()
    {
    }

    public void AddTranslation(string languageCode, string name, string userContext)
    {
        _translations.Add(new SituationVariantTranslation(languageCode, name, userContext));
    }
}

public sealed class SituationVariantTranslation : TranslationBase
{
    public string Name { get; private set; }
    public string UserContext { get; private set; }

    public SituationVariantTranslation(string languageCode, string name, string userContext)
    {
        LanguageCode = languageCode;
        Name = name;
        UserContext = userContext;
    }

    private SituationVariantTranslation()
    {
    }
}