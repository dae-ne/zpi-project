using Dietly.WebApi.Endpoints.Recipes.GetRecipe;

namespace Dietly.WebApi.Endpoints.Meals.GetMeal;

public sealed class GetMealResponse
{
    public int Id { get; init; }
    
    public int RecipeId { get; init; }
    
    public string Date { get; init; } = null!;
    
    public bool Completed { get; init; }
    
    public GetRecipeResponse Recipe { get; init; } = null!;
}
