using Dietly.Domain.Common;

namespace Dietly.Domain.Entities;

public class User : BaseCloneableEntity
{
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? AvatarUrl { get; set; }
}
