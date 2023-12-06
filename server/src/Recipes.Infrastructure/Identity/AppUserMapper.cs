using Recipes.Domain.Entities;

namespace Recipes.Infrastructure.Identity;

public static class AppUserMapper
{
    public static User ToDomain(this AppUser appUser) => new()
    {
        Id = appUser.Id,
        UserName = appUser.UserName ?? "",
        Email = appUser.Email ?? "",
        AvatarUrl = appUser.AvatarUrl ?? ""
    };
}
