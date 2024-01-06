using Dietly.Application.Users.Commands.UpdateUser;

namespace Dietly.WebApi.Resources.Users.UpdateUser;

internal static class UpdateUserMapper
{
    public static UpdateUserCommand ToCommand(this UpdateUserRequest request, int id) => new()
    {
        UserId = id,
        UserName = request.UserName,
        AvatarUrl = request.AvatarUrl
    };
}
