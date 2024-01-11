using Dietly.Application.Users.Commands.RemoveUser;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;

namespace Dietly.WebApi.Resources.Users.Delete;

public sealed class UserDeleteEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Delete("/api/users/{userId}")
        .WithTags("Users")
        .WithName("removeUser")
        .Produces(200);

    public async Task<IResult> HandleAsync(int userId)
    {
        var currentUserId = currentUser.GetId();

        if (currentUserId != userId)
        {
            return Results.Problem(statusCode: 403);
        }

        var command = new RemoveUserCommand(userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
