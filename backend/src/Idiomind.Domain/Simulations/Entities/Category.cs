namespace Idiomind.Domain.Simulations.Entities;

public sealed class Category : BaseEntity, ITranslatable<CategoryTranslation>
{
    public string Icon { get; private set; }

    private readonly List<CategoryTranslation> _translations = new();
    public IReadOnlyCollection<CategoryTranslation> Translations => _translations;

    public Category(string? icon = null, List<CategoryTranslation>? translations = null)
    {
        Icon = icon;
        _translations = translations ?? new();
    }

    private Category()
    {
    }

    public void AddTranslation(string languageCode, string name)
    {
        _translations.Add(new CategoryTranslation(languageCode, name));
    }
}

public sealed class CategoryTranslation : TranslationBase
{
    public string Name { get; private set; }

    public CategoryTranslation(string languageCode, string name)
    {
        LanguageCode = languageCode;
        Name = name;
    }

    private CategoryTranslation()
    {
    }
}