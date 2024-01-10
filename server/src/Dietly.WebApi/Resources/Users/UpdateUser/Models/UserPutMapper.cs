using Dietly.Application.Users.Commands.UpdateUser;

namespace Dietly.WebApi.Resources.Users.UpdateUser.Models;

internal static class UserPutMapper
{
    public static UpdateUserCommand ToCommand(this UserPutRequest request, int id) => new()
    {
        UserId = id,
        UserName = request.UserName,
        AvatarUrl = request.AvatarUrl
    };
}
