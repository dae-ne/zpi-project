using Dietly.Application.Meals.Commands.UpdateMeal;

namespace Dietly.WebApi.Endpoints.Meals.UpdateMeal;

internal static class UpdateMealMapper
{
    public static UpdateMealCommand ToCommand(this UpdateMealRequest request, int mealId, int userId) => new()
    {
        MealId = mealId,
        Date = DateTime.Parse(request.Date),
        Completed = request.Completed,
        UserId = userId
    };
}
