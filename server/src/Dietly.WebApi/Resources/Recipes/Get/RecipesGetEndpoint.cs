using Dietly.Application.Recipes.Queries.GetRecipes;
using Dietly.WebApi.Resources.Recipes.Get.Models;

namespace Dietly.WebApi.Resources.Recipes.Get;

[ApiEndpointGet("/api/recipes")]
public sealed class RecipesGetEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("getRecipes")
        .Produces<RecipesGetResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync()
    {
        var userId = currentUser.GetId();
        var query = new GetRecipesQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(RecipesGetMapper.ToDto);
    }
}
