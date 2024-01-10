namespace Dietly.WebApi.Resources.Meals.Post.Models;

public sealed class MealPostRequest
{
    public int RecipeId { get; init; }

    public string Date { get; init; } = null!;
}
