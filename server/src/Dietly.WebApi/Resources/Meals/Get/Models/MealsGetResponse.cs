using System.Collections.Generic;

namespace Dietly.WebApi.Resources.Meals.Get.Models;

public sealed class MealsGetResponse
{
    public int Count { get; init; }

    public IEnumerable<MealGetResponse> Data { get; init; } = Enumerable.Empty<MealGetResponse>();
}
