namespace Dietly.Application.Users.Commands.UpdateUser;

internal static class UpdateUserMapper
{
    public static User ToDomain(this UpdateUserCommand command) => new()
    {
        Id = command.UserId,
        UserName = command.UserName,
        AvatarUrl = command.AvatarUrl
    };
}
