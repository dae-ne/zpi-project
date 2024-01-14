using Dietly.Application.Users.Queries.GetUser;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Users.Get.Models;

namespace Dietly.WebApi.Resources.Users.Get;

public sealed class UserGetEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/api/users/{userId}")
        .WithTags("Users")
        .WithName("getUser")
        .DisableAntiforgery()
        .Produces<UserGetResponse>()
        .ProducesProblem(403)
        .ProducesValidationProblem();

    public async Task<IResult> HandleAsync(int userId)
    {
        var currentUserId = currentUser.GetId();

        if (currentUserId != userId)
        {
            return Results.Forbid();
        }

        var query = new GetUserQuery(userId);
        var result = await mediator.Send(query);
        return result.Match(user => Results.Ok(user.ToDto()), HandleError);
    }
}
