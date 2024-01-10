namespace Dietly.WebApi.Resources.Users.Get.Models;

public sealed class UserGetResponse
{
    public int Id { get; init; }

    public string UserName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string? AvatarUrl { get; init; }
}
