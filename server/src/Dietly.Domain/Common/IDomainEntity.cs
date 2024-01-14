namespace Dietly.Domain.Common;

public interface IDomainEntity
{
    IReadOnlyCollection<BaseEvent> DomainEvents { get; }

    void AddDomainEvent(BaseEvent domainEvent);

    void ClearDomainEvents();
}
