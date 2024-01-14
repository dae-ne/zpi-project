using Dietly.Domain.Common;

namespace Dietly.Domain.Events.User;

public class UserRemovedEvent(Entities.User item) : BaseEvent
{
    public Entities.User Item { get; } = item;
}
