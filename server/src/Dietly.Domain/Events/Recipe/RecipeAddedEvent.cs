using Dietly.Domain.Common;

namespace Dietly.Domain.Events.Recipe;

public class RecipeAddedEvent(Entities.Recipe item) : BaseEvent
{
    public Entities.Recipe Item { get; } = item;
}
