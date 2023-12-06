using Recipes.Domain.Common;

namespace Recipes.Domain.Entities;

public class User : BaseCloneableEntity
{
    public string UserName { get; set; } = "";
    
    public string Email { get; set; } = "";
    
    public string? AvatarUrl { get; set; } = "";
}
