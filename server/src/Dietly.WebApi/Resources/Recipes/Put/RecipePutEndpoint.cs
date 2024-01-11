using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;
using Dietly.WebApi.Resources.Recipes.Put.Models;

namespace Dietly.WebApi.Resources.Recipes.Put;

public sealed class RecipePutEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Put("/api/recipes/{recipeId}")
        .WithTags("Recipes")
        .WithName("updateRecipe")
        .Produces(200);

    public async Task<IResult> HandleAsync(int recipeId, RecipePutRequest request, HttpContext httpContext)
    {
        var userId = currentUser.GetId();

        if (request.Id != recipeId)
        {
            return Results.Problem(
                statusCode: 400,
                detail: "The recipe ID provided in the URL does not match the one provided in the request body");
        }

        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
