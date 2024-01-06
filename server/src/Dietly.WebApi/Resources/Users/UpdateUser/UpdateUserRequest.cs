namespace Dietly.WebApi.Resources.Users.UpdateUser;

public sealed class UpdateUserRequest
{
    public int Id { get; init; }

    public string UserName { get; init; } = null!;

    public string? AvatarUrl { get; init; }
}
