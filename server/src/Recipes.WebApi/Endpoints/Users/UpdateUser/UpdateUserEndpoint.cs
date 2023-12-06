namespace Recipes.WebApi.Endpoints.Users.UpdateUser;

[ApiEndpointPut("/api/users/{id}")]
public sealed class UpdateUserEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Users")
        .WithName("updateUser");
    
    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int id, UpdateUserRequest request)
    {
        var userId = currentUser.GetId();
        
        if (userId != id)
        {
            return Results.Forbid();
        }
        
        var command = request.ToCommand(id);
        await mediator.Send(command);
        return Results.Ok();
    }
}
