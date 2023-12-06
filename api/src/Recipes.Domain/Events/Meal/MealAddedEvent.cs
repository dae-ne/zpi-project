using Recipes.Domain.Common;

namespace Recipes.Domain.Events.Meal;

public sealed class MealAddedEvent(Entities.Meal item) : BaseEvent
{
    public Entities.Meal Item { get; } = item;
}
