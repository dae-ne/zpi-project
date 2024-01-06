using Dietly.Application.Users.Queries.GetUser;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Users.GetUser;

// It's an alias for the standard /api/users/{userId} endpoint.
// The only difference is that it uses the current user's ID instead of the one provided in the URL.
[ApiEndpointGet("/api/users/me")]
public sealed class GetCurrentUserEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("getCurrentUser")
        .Produces<GetUserResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync()
    {
        var userId = currentUser.GetId();
        var query = new GetUserQuery(userId);
        var user = await mediator.Send(query);
        var dto = user.ToDto();
        return Results.Ok(dto);
    }
}
