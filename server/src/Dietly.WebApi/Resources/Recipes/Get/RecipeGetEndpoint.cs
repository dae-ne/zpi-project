using Dietly.Application.Recipes.Queries.GetRecipe;
using Dietly.WebApi.Resources.Recipes.Get.Models;

namespace Dietly.WebApi.Resources.Recipes.Get;

[ApiEndpointGet("/api/recipes/{recipeId}")]
public sealed class RecipeGetEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("getRecipe")
        .Produces<RecipeGetResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int recipeId)
    {
        var userId = currentUser.GetId();
        var query = new GetRecipeQuery(recipeId, userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(Mapper.ToDto);
    }
}
