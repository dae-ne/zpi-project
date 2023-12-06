using Recipes.Application.Meals.Commands.AddMeal;

namespace Recipes.WebApi.Endpoints.Meals.AddMeal;

internal static class AddMealMapper
{
    public static AddMealCommand ToCommand(this AddMealRequest request, int userId) => new()
    {
        UserId = userId,
        RecipeId = request.RecipeId,
        Date = DateTime.Parse(request.Date)
    };
}