using Recipes.Application.Users.Commands.RemoveUser;

namespace Recipes.WebApi.Endpoints.Users.RemoveUser;

[ApiEndpointDelete("/api/users/{userId}")]
public sealed class RemoveUserEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("removeUser");
    
    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int userId)
    {
        var currentUserId = currentUser.GetId();
        
        if (currentUserId != userId)
        {
            return Results.Forbid();
        }
        
        var command = new RemoveUserCommand(userId);
        await mediator.Send(command);
        return Results.Ok();
    }
}
