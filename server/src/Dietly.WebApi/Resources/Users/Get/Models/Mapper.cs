using Dietly.Domain.Entities;

namespace Dietly.WebApi.Resources.Users.Get.Models;

internal static class Mapper
{
    public static UserGetResponse ToDto(this User user) => new()
    {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        AvatarUrl = user.AvatarUrl
    };
}
