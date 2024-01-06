namespace Recipes.WebApi.Endpoints.Users.GetUser;

public sealed class GetUserResponse
{
    public int Id { get; init; }
    
    public string UserName { get; init; } = null!;
    
    public string Email { get; init; } = null!;
    
    public string? AvatarUrl { get; init; }
}
