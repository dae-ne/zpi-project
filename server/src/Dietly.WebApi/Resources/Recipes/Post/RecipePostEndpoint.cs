using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Recipes.Post.Models;

namespace Dietly.WebApi.Resources.Recipes.Post;

public sealed class RecipePostEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Post("/api/recipes")
        .WithTags("Recipes")
        .WithName("createRecipe")
        .Produces(201);

    public async Task<IResult> HandleAsync(RecipePostRequest request, HttpContext httpContext) // TODO: get just the request
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);

        return result.Match(
            id => Results.Created(httpContext.Request.GenerateUrlForCreatedItem(id), null),
            HandleError);
    }
}
