namespace Dietly.WebApi.Resources.Users.Put.Models;

public sealed class UserPutRequest
{
    public int Id { get; init; }

    public string UserName { get; init; } = null!;

    public string? AvatarUrl { get; init; }
}
