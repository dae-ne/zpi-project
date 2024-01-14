using Dietly.Domain.Common;

namespace Dietly.Domain.Events.Meal;

public sealed class MealRemovedEvent(Entities.Meal item) : BaseEvent
{
    public Entities.Meal Item { get; } = item;
}
