using Dietly.Application.Users.Queries.GetUser;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Users.GetUser;

[ApiEndpointGet("/api/users/{userId}")]
public sealed class GetUserEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("getUser")
        .Produces<GetUserResponse>(200, "application/json")
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
        var user = await mediator.Send(query);
        var dto = user.ToDto();
        return Results.Ok(dto);
    }
}
