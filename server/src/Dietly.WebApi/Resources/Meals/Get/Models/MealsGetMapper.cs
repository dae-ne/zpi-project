using System.Collections.Generic;
using Dietly.Application.Meals.Queries.GetMeals;
using Dietly.Domain.Entities;
using Dietly.WebApi.Resources.Recipes.Get.Models;

namespace Dietly.WebApi.Resources.Meals.Get.Models;

internal static class MealsGetMapper
{
    public static MealGetResponse ToDto(this Meal meal) => new()
    {
        Id = meal.Id,
        RecipeId = meal.RecipeId,
        Date = meal.Date
            .ToUniversalTime()
            .ToString("u")
            .Replace(" ", "T"),
        Completed = meal.Completed,
        Recipe = meal.Recipe.ToDto()
    };

    public static GetMealsQuery ToQuery(this MealsGetQueryParams queryParams, int userId) => new()
    {
        UserId = userId,
        StartDate = DateOnly.Parse(queryParams.From ?? DateOnly.MinValue.ToString()),
        EndDate = DateOnly.Parse(queryParams.To ?? DateOnly.MaxValue.ToString())
    };

    public static MealsGetResponse ToDto(this IList<Meal> meals) => new()
    {
        Count = meals.Count,
        Data = meals.Select(x => x.ToDto())
    };
}
