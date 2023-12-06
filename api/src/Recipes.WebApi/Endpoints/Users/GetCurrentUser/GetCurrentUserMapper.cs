using Recipes.Domain.Entities;

namespace Recipes.WebApi.Endpoints.Users.GetCurrentUser;

internal static class GetCurrentUserMapper
{
    public static GetCurrentUserResponse ToDto(this User user) => new()
    {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        AvatarUrl = user.AvatarUrl
    };
}
