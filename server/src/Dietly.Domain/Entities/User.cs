using Dietly.Domain.Common;

namespace Dietly.Domain.Entities;

public class User : BaseCloneableEntity
{
    public string UserName { get; set; } = "";
    
    public string Email { get; set; } = "";
    
    public string? AvatarUrl { get; set; } = "";
}
