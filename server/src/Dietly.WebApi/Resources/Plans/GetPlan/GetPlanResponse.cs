using System.Collections.Generic;
using Dietly.WebApi.Resources.Meals.GetMeal;

namespace Dietly.WebApi.Resources.Plans.GetPlan;

public sealed class GetPlanResponse
{
    public IEnumerable<GetMealResponse> Meals { get; init; } = Enumerable.Empty<GetMealResponse>();

    public string Date { get; init; } = null!;

    public int TotalCalories { get; init; }

    public int ConsumedCalories { get; init; }
}