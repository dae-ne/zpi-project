namespace Recipes.Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public string Name { get; set; } = null!;
    
    public List<Recipe> Recipes { get; set; } = new();
}
