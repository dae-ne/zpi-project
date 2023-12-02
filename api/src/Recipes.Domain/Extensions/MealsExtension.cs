using Recipes.Domain.Entities;
using Recipes.Domain.Models;

namespace Recipes.Domain.Extensions;

public static class MealsExtension
{
    public static IEnumerable<DayPlan> ToDayPlans(this IList<Meal> meals, DateTime from, DateTime to)
    {
        if (!meals.Any())
        {
            return Enumerable.Empty<DayPlan>();
        }
        
        if (from > to)
        {
            throw new ArgumentException("From date must be before to date");
        }
        
        var userIds = meals
            .Select(m => m.Recipe.UserId)
            .Distinct();
        
        if (userIds.Count() > 1)
        {
            throw new InvalidOperationException("Meals must be from the same user");
        }
        
        var plans = meals
            .GroupBy(m => DateOnly.FromDateTime(m.Date.Date))
            .Select(CreateDayPlanFromGroup);

        return plans;
    }
    
    private static DayPlan CreateDayPlanFromGroup(IGrouping<DateOnly, Meal> group)
    {
        var totalCalories = group
            .Sum(m => m.Recipe.Calories);
        var consumedCalories = group
            .Where(g => g.Completed)
            .Sum(m => m.Recipe.Calories);
        
        return new DayPlan
        {
            Meals = group,
            Date = group.Key,
            TotalCalories = totalCalories,
            ConsumedCalories = consumedCalories
        };
    }
}
