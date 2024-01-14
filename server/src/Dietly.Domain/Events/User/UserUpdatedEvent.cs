using Dietly.Domain.Common;

namespace Dietly.Domain.Events.User;

public class UserUpdatedEvent(Entities.User oldItem, Entities.User newItem) : BaseEvent
{
    public Entities.User OldItem { get; } = oldItem;

    public Entities.User NewItem { get; } = newItem;
}
