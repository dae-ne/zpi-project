using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Dietly.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Dietly.Infrastructure.Identity;

public sealed class AppUser : IdentityUser<int>, IDomainEntity, ICloneable
{
    private readonly List<BaseEvent> _domainEvents = [];

    // TODO: add other properties
    public string? AvatarUrl { get; set; }

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();

    public object Clone()
    {
        var json = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize(json, GetType())!;
    }
}
