namespace Recipes.Domain.Entities;

public class Meal
{
    public int Id { get; set; }
    
    public int RecipeId { get; set; }
    
    public DateTime Date { get; set; }
    
    public bool Completed { get; set; }
    
    public Recipe Recipe { get; set; } = null!;
}
