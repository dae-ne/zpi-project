namespace Dietly.WebApi.Resources.Recipes.CreateRecipe;

[ApiEndpointPost("/api/recipes")]
public sealed class CreateRecipeEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("createRecipe")
        .Produces(201);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(CreateRecipeRequest request, HttpContext httpContext) // TODO: get just the request
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult(httpContext.Request.GenerateUrlForCreatedItem);
    }
}
