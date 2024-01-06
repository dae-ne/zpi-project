using System.Collections.Generic;
using Dietly.Application.Meals.Queries.GetMeals;
using Dietly.Domain.Entities;
using Dietly.WebApi.Resources.Meals.GetMeal;

namespace Dietly.WebApi.Resources.Meals.GetMeals;

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
