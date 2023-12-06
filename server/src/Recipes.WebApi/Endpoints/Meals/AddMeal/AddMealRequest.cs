namespace Recipes.WebApi.Endpoints.Meals.AddMeal;

public sealed class AddMealRequest
{
    public int RecipeId { get; init; }

    public string Date { get; init; } = null!;
}
