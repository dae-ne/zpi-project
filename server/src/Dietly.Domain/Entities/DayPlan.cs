using Dietly.Domain.Common;

namespace Dietly.Domain.Entities;

public class DayPlan : BaseCloneableEntity
{
    public List<Meal> Meals { get; set; } = [];

    public DateOnly Date { get; set; }

    public int TotalCalories { get; set; }

    public int ConsumedCalories { get; set; }
}
