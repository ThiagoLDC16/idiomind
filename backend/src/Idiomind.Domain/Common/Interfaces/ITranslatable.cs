namespace Idiomind.Domain.Common.Interfaces;

public interface ITranslatable<TTranslation>
    where TTranslation : class
{
    IReadOnlyCollection<TTranslation> Translations { get; }
}