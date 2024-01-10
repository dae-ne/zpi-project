using Dietly.Application.Users.Queries.GetUser;
using Dietly.WebApi.Resources.Users.Get.Models;

namespace Dietly.WebApi.Resources.Users.Get;

// It's an alias for the standard /api/users/{userId} endpoint.
// The only difference is that it uses the current user's ID instead of the one provided in the URL.
[ApiEndpointGet("/api/users/me")]
public sealed class CurrentUserGetEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("getCurrentUser")
        .Produces<UserGetResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync()
    {
        var userId = currentUser.GetId();
        var query = new GetUserQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(UserGetMapper.ToDto);
    }
}
