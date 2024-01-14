using Dietly.Domain.Common;

namespace Dietly.Domain.Events.Meal;

public sealed class MealUpdatedEvent(Entities.Meal oldItem, Entities.Meal newItem) : BaseEvent
{
    public Entities.Meal OldItem { get; } = oldItem;

    public Entities.Meal NewItem { get; } = newItem;
}
