using Recipes.Application.Users.Commands.UpdateUser;

namespace Recipes.WebApi.Endpoints.Users.UpdateUser;

internal static class UpdateUserMapper
{
    public static UpdateUserCommand ToCommand(this UpdateUserRequest request, int id) => new()
    {
        UserId = id,
        UserName = request.UserName,
        AvatarUrl = request.AvatarUrl
    };
}
