using System.Collections.Generic;
using Dietly.WebApi.Resources.Meals.Get.Models;

namespace Dietly.WebApi.Resources.Plans.Get.Models;

public sealed class PlanGetResponse
{
    public IEnumerable<MealGetResponse> Meals { get; init; } = Enumerable.Empty<MealGetResponse>();

    public string Date { get; init; } = null!;

    public int TotalCalories { get; init; }

    public int ConsumedCalories { get; init; }
}
