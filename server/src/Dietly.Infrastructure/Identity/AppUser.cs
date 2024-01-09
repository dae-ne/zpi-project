using Microsoft.AspNetCore.Identity;

namespace Dietly.Infrastructure.Identity;

public sealed class AppUser : IdentityUser<int>
{
    // TODO: add other properties
    public string? AvatarUrl { get; set; }
}
