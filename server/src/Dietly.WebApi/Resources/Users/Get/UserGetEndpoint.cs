using Dietly.Application.Users.Queries.GetUser;
using Dietly.WebApi.Resources.Users.Get.Models;

namespace Dietly.WebApi.Resources.Users.Get;

[ApiEndpointGet("/api/users/{userId}")]
public sealed class UserGetEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("getUser")
        .Produces<UserGetResponse>(200, "application/json")
        .Produces(403);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int userId)
    {
        var currentUserId = currentUser.GetId();

        if (currentUserId != userId)
        {
            return Results.Forbid();
        }

        var query = new GetUserQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(UserGetMapper.ToDto);
    }
}
