using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Users.Put.Models;

namespace Dietly.WebApi.Resources.Users.Put;

public sealed class UserPutEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Put("/api/users/{userId}")
        .WithTags("Users")
        .WithName("updateUser")
        .Produces(200);

    public async Task<IResult> HandleAsync(int userId, UserPutRequest request)
    {
        var currentUserId = currentUser.GetId();

        if (currentUserId != userId ||
            currentUserId != request.Id)
        {
            return Results.Problem(statusCode: 403);
        }

        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);
        return result.Match(Results.Ok, HandleError);
    }
}
