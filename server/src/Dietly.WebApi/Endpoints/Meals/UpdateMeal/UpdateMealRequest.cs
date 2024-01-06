namespace Dietly.WebApi.Endpoints.Meals.UpdateMeal;

public sealed class UpdateMealRequest
{
    public int Id { get; init; }

    public int RecipeId { get; init; }

    public string Date { get; init; } = null!;

    public bool Completed { get; init; }
}
