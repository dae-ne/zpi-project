using Recipes.Domain.Enums;

namespace Recipes.Domain.Entities;

public class Recipe
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public string Title { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public DifficultyLevel DifficultyLevel { get; set; }
    
    public string ImageUrl { get; set; } = null!;
    
    public TimeSpan Time { get; set; }
    
    public int Calories { get; set; }
    
    public List<Ingredient> Ingredients { get; set; } = new();
    
    public List<Direction> Directions { get; set; } = new();
    
    public List<Tag> Tags { get; set; } = new();
}
