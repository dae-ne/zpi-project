using Dietly.Domain.Common;

namespace Dietly.Domain.Events.Recipe;

public class RecipeRemovedEvent(Entities.Recipe item) : BaseEvent
{
    public Entities.Recipe Item { get; } = item;
}
