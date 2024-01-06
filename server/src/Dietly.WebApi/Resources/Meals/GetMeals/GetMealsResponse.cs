using System.Collections.Generic;
using Dietly.WebApi.Resources.Meals.GetMeal;

namespace Dietly.WebApi.Resources.Meals.GetMeals;

public sealed class GetMealsResponse
{
    public int Count { get; init; }

    public IEnumerable<GetMealResponse> Data { get; init; } = Enumerable.Empty<GetMealResponse>();
}
