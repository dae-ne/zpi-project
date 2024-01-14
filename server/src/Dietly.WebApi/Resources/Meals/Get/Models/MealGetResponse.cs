using Dietly.WebApi.Resources.Recipes.Get.Models;

namespace Dietly.WebApi.Resources.Meals.Get.Models;

public sealed class MealGetResponse
{
    public int Id { get; init; }

    public int RecipeId { get; init; }

    public string Date { get; init; } = null!;

    public bool Completed { get; init; }

    public RecipeGetResponse Recipe { get; init; } = null!;
}
