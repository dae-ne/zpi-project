using Recipes.Application.Users.Queries.GetUser;

namespace Recipes.WebApi.Endpoints.Users.GetCurrentUser;

[ApiEndpointGet("/api/users/me")]
public sealed class GetCurrentUserEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("getCurrentUser")
        .Produces<GetCurrentUserResponse>(200, "application/json");

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
