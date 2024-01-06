using Dietly.Domain.Entities;
using Dietly.WebApi.Endpoints.Meals.GetMeal;

namespace Dietly.WebApi.Endpoints.Plans.GetPlan;

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
