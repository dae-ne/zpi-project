using System.ComponentModel.DataAnnotations.Schema;

namespace Dietly.Domain.Common;

public abstract class BaseEntity : BaseEntity<int>;

public abstract class BaseEntity<TKey> : IDomainEntity
    where TKey : IEquatable<TKey>
{
    private readonly List<BaseEvent> _domainEvents = [];

    public TKey Id { get; set; } = default!;

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
