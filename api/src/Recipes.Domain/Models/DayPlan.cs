using Recipes.Domain.Entities;

namespace Recipes.Domain.Models;

public class DayPlan
{
    public IEnumerable<Meal> Meals { get; init; } = null!;
    
    public DateOnly Date { get; init; }
    
    public int TotalCalories { get; init; }
    
    public int ConsumedCalories { get; init; }
}
