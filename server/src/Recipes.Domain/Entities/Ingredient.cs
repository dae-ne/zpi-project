using Recipes.Domain.Common;

namespace Recipes.Domain.Entities;

public class Ingredient : BaseCloneableEntity
{
    public int UserId { get; set; }
    
    public string Name { get; set; } = null!;
}
