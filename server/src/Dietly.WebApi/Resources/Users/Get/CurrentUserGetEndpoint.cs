using Dietly.Application.Users.Queries.GetUser;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Users.Get.Models;

namespace Dietly.WebApi.Resources.Users.Get;

// It's an alias for the standard /api/users/{userId} endpoint.
// The only difference is that it uses the current user's ID instead of the one provided in the URL.
public sealed class CurrentUserGetEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/api/users/me")
        .WithTags("Users")
        .WithName("getCurrentUser")
        .Produces<UserGetResponse>();

    public async Task<IResult> HandleAsync()
    {
        var userId = currentUser.GetId();
        var query = new GetUserQuery(userId);
        var result = await mediator.Send(query);
        return result.Match(user => Results.Ok(user.ToDto()), HandleError);
    }
}
