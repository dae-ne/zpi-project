using Recipes.Domain.Entities;
using Recipes.WebApi.Endpoints.Meals.GetMeal;

namespace Recipes.WebApi.Endpoints.Plans.GetPlan;

internal static class GetPlanMapper
{
    public static GetPlanResponse ToDto(this DayPlan plan) => new()
    {
        Date = plan.Date.ToString("yyyy-MM-dd"),
        TotalCalories = plan.TotalCalories,
        ConsumedCalories = plan.ConsumedCalories,
        Meals = plan.Meals.Select(x => x.ToDto())
    };
}
