namespace Recipes.Domain.Entities;

public class Direction
{
    public int Id { get; set; }
    
    public int RecipeId { get; set; }
    
    public string Description { get; set; } = null!;
    
    public int Order { get; set; }
}
