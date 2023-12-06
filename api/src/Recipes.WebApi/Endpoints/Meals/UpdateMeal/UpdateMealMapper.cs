using Recipes.Application.Meals.Commands.UpdateMeal;

namespace Recipes.WebApi.Endpoints.Meals.UpdateMeal;

internal static class UpdateMealMapper
{
    public static UpdateMealCommand ToCommand(this UpdateMealRequest request, int mealId, int userId) => new()
    {
        MealId = mealId,
        Date = request.Date is null ? null : DateTime.Parse(request.Date),
        Completed = request.Completed,
        UserId = userId
    };
}
