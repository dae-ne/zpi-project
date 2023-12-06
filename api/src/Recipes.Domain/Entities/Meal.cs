using Recipes.Domain.Common;

namespace Recipes.Domain.Entities;

public class Meal : BaseCloneableEntity
{
    public int RecipeId { get; set; }
    
    public DateTime Date { get; set; }
    
    public bool Completed { get; set; }
    
    public Recipe? Recipe { get; set; }
}
