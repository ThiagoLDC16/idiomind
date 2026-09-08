namespace Idiomind.Domain.Common.Base;

public abstract class AggregateRoot : BaseEntity
{
    private readonly List<BaseDomainEvent> _domainEvents = new();

    [NotMapped] public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents;

    public void AddDomainEvent(BaseDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}