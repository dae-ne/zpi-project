using Recipes.Domain.Common;

namespace Recipes.Domain.Entities;

public class Direction : BaseCloneableEntity
{
    public int RecipeId { get; set; }
    
    public string Description { get; set; } = null!;
    
    public int Order { get; set; }
}
