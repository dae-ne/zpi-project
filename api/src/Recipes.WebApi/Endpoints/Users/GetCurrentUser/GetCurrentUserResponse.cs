namespace Recipes.WebApi.Endpoints.Users.GetCurrentUser;

public sealed class GetCurrentUserResponse
{
    public int Id { get; init; }
    
    public string UserName { get; init; } = null!;
    
    public string Email { get; init; } = null!;
    
    public string? AvatarUrl { get; init; }
}
