using Dietly.Domain.Entities;

namespace Dietly.Infrastructure.Identity;

public static class AppUserMapper
{
    public static User ToDomain(this AppUser appUser) => new()
    {
        Id = appUser.Id,
        UserName = appUser.UserName ?? string.Empty,
        Email = appUser.Email ?? string.Empty,
        AvatarUrl = appUser.AvatarUrl ?? string.Empty
    };
}
