namespace Idiomind.Domain.Common.Base;

public abstract class TranslationBase : BaseEntity
{
    public Guid EntityId { get; protected set; }
    public string LanguageCode { get; protected set; } = null!;
}