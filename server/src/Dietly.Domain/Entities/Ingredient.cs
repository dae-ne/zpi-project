using Dietly.Domain.Common;

namespace Dietly.Domain.Entities;

public class Ingredient : BaseCloneableEntity
{
    public int UserId { get; set; }
    
    public string Name { get; set; } = null!;
}
