using Dietly.Domain.Entities;

namespace Dietly.Domain.Extensions;

public static class MealsExtensions
{
    public static DayPlan ToDayPlan(this IEnumerable<Meal> meals, DateOnly day, int userId)
    {
        return meals.ToDayPlans(day, day, userId).Single();
    }

    public static IEnumerable<DayPlan> ToDayPlans(this IEnumerable<Meal> meals, DateOnly from, DateOnly to, int userId)
    {
        var mealList = meals
            .Where(m => m.Recipe.UserId == userId)
            .Where(m => m.Date.Date >= from.ToDateTime(TimeOnly.MinValue) &&
                        m.Date.Date <= to.ToDateTime(TimeOnly.MaxValue))
            .ToList();

        if (mealList.Count < 1)
        {
            return Enumerable.Empty<DayPlan>();
        }

        var plans = mealList
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
            Meals = group.ToList(),
            Date = group.Key,
            TotalCalories = totalCalories,
            ConsumedCalories = consumedCalories
        };
    }
}
