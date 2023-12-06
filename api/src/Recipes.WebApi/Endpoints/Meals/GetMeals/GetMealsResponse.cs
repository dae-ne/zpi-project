using System.Collections.Generic;
using Recipes.WebApi.Endpoints.Meals.GetMeal;

namespace Recipes.WebApi.Endpoints.Meals.GetMeals;

public sealed class GetMealsResponse
{
    public int Count { get; init; }
    
    public IEnumerable<GetMealResponse> Data { get; init; } = Enumerable.Empty<GetMealResponse>();
}
