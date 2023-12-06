namespace Recipes.WebApi.Endpoints.Users.UpdateUser;

public sealed class UpdateUserRequest
{
    public string? UserName { get; init; }
    
    public string? AvatarUrl { get; init; }
}
