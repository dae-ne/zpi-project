namespace Dietly.WebApi.Resources.Meals.Put.Models;

public sealed class MealPutRequest
{
    public int Id { get; init; }

    public int RecipeId { get; init; }

    public string Date { get; init; } = null!;

    public bool Completed { get; init; }
}
