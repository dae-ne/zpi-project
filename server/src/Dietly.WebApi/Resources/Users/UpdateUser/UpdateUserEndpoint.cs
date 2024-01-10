namespace Dietly.WebApi.Resources.Users.UpdateUser;

[ApiEndpointPut("/api/users/{userId}")]
public sealed class UpdateUserEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("updateUser")
        .Produces(200);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int userId, UpdateUserRequest request)
    {
        var currentUserId = currentUser.GetId();

        if (currentUserId != userId ||
            currentUserId != request.Id)
        {
            return Results.Problem(statusCode: 403);
        }

        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
