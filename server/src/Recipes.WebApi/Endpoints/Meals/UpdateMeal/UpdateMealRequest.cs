namespace Recipes.WebApi.Endpoints.Meals.UpdateMeal;

public sealed class UpdateMealRequest
{
    public int? RecipeId { get; init; }
    
    public string? Date { get; init; }
    
    public bool? Completed { get; init; }
}
