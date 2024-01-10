using Dietly.Application.Users.Queries.GetUser;

namespace Dietly.WebApi.Resources.Users.GetUser;

[ApiEndpointGet("/api/users/{userId}")]
public sealed class GetUserEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
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
        var result = await mediator.Send(query);
        return result.ToHttpResult(GetUserMapper.ToDto);
    }
}
