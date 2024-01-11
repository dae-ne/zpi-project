using Dietly.Application.Users.Commands.UpdateUser;

namespace Dietly.WebApi.Resources.Users.Put.Models;

internal static class Mapper
{
    public static UpdateUserCommand ToCommand(this UserPutRequest request, int id) => new()
    {
        UserId = id,
        UserName = request.UserName,
        AvatarUrl = request.AvatarUrl
    };
}
