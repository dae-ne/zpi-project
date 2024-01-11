using Dietly.Application.Recipes.Queries.GetRecipe;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;
using Dietly.WebApi.Resources.Recipes.Get.Models;

namespace Dietly.WebApi.Resources.Recipes.Get;

public sealed class RecipeGetEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/api/recipes/{recipeId}")
        .WithTags("Recipes")
        .WithName("getRecipe")
        .Produces<RecipeGetResponse>();

    public async Task<IResult> HandleAsync(int recipeId)
    {
        var userId = currentUser.GetId();
        var query = new GetRecipeQuery(recipeId, userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(Mapper.ToDto);
    }
}
