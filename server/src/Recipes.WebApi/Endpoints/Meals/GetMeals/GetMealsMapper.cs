using System.Collections.Generic;
using Recipes.Application.Meals.Queries.GetMeals;
using Recipes.Domain.Entities;
using Recipes.WebApi.Endpoints.Meals.GetMeal;

namespace Recipes.WebApi.Endpoints.Meals.GetMeals;

internal static class GetMealsMapper
{
    public static GetMealsQuery ToQuery(this GetMealsQueryParams queryParams, int userId) => new()
    {
        UserId = userId,
        StartDate = DateOnly.Parse(queryParams.From ?? DateOnly.MinValue.ToString()),
        EndDate = DateOnly.Parse(queryParams.To ?? DateOnly.MaxValue.ToString())
    };

    public static GetMealsResponse ToDto(this IList<Meal> meals) => new()
    {
        Count = meals.Count,
        Data = meals.Select(x => x.ToDto())
    };
}
