using Dietly.Application.Users.Commands.RemoveUser;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Endpoints.Users.RemoveUser;

[ApiEndpointDelete("/api/users/{userId}")]
public sealed class RemoveUserEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("removeUser")
        .Produces(200)
        .Produces(403);

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
