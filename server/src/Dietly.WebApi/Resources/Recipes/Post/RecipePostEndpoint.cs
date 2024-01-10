using Dietly.WebApi.Resources.Recipes.Post.Models;

namespace Dietly.WebApi.Resources.Recipes.Post;

[ApiEndpointPost("/api/recipes")]
public sealed class RecipePostEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("createRecipe")
        .Produces(201);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(RecipePostRequest request, HttpContext httpContext) // TODO: get just the request
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult(httpContext.Request.GenerateUrlForCreatedItem);
    }
}
