using Dietly.Application.Meals.Commands.AddMeal;

namespace Dietly.WebApi.Resources.Meals.Post.Models;

internal static class Mapper
{
    public static AddMealCommand ToCommand(this MealPostRequest request, int userId) => new()
    {
        UserId = userId,
        RecipeId = request.RecipeId,
        Date = DateTime.Parse(request.Date)
    };
}
