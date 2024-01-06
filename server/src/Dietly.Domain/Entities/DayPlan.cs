using Dietly.Domain.Common;

namespace Dietly.Domain.Entities;

public class DayPlan : BaseCloneableEntity
{
    public IEnumerable<Meal> Meals { get; set; } = Enumerable.Empty<Meal>();

    public DateOnly Date { get; set; }

    public int TotalCalories { get; set; }

    public int ConsumedCalories { get; set; }
}
