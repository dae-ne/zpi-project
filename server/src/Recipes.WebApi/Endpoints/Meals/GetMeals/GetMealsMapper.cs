using System.Collections.Generic;
using Recipes.Domain.Entities;
using Recipes.WebApi.Endpoints.Meals.GetMeal;

namespace Recipes.WebApi.Endpoints.Meals.GetMeals;

internal static class GetMealsMapper
{
    public static GetMealsResponse ToDto(this IList<Meal> meals) => new()
    {
        Count = meals.Count,
        Data = meals.Select(x => x.ToDto())
    };
}
