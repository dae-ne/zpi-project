using Dietly.Domain.Entities;

namespace Dietly.WebApi.Resources.Users.GetUser;

internal static class GetUserMapper
{
    public static GetUserResponse ToDto(this User user) => new()
    {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        AvatarUrl = user.AvatarUrl
    };
}
