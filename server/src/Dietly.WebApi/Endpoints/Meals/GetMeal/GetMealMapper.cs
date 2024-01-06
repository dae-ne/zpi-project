using Dietly.Domain.Entities;
using Dietly.WebApi.Endpoints.Recipes.GetRecipe;

namespace Dietly.WebApi.Endpoints.Meals.GetMeal;

internal static class GetMealMapper
{
    public static GetMealResponse ToDto(this Meal meal) => new()
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
}
