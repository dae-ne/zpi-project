using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Endpoints.Users.UpdateUser;

[ApiEndpointPut("/api/users/{userId}")]
public sealed class UpdateUserEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("updateUser")
        .Produces(200)
        .Produces(403);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int userId, UpdateUserRequest request)
    {
        var currentUserId = currentUser.GetId();

        if (currentUserId != userId)
        {
            return Results.Forbid();
        }

        var command = request.ToCommand(userId);
        await mediator.Send(command);
        return Results.Ok();
    }
}
