using Dietly.Domain.Common;

namespace Dietly.Domain.Events.Recipe;

public class RecipeUpdatedEvent(Entities.Recipe oldItem, Entities.Recipe newItem) : BaseEvent
{
    public Entities.Recipe OldItem { get; } = oldItem;

    public Entities.Recipe NewItem { get; } = newItem;
}
