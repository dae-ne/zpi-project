using Dietly.Application.Meals.Commands.UpdateMeal;

namespace Dietly.WebApi.Resources.Meals.Put.Models;

internal static class MealPutMapper
{
    public static UpdateMealCommand ToCommand(this MealPutRequest request, int mealId, int userId) => new()
    {
        MealId = mealId,
        Date = DateTime.Parse(request.Date),
        Completed = request.Completed,
        UserId = userId
    };
}
