using System.Collections.Generic;
using Dietly.WebApi.Endpoints.Meals.GetMeal;

namespace Dietly.WebApi.Endpoints.Meals.GetMeals;

public sealed class GetMealsResponse
{
    public int Count { get; init; }
    
    public IEnumerable<GetMealResponse> Data { get; init; } = Enumerable.Empty<GetMealResponse>();
}
