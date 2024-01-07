using Dietly.Application.Recipes.Queries.GetRecipe;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Recipes.GetRecipe;

[ApiEndpointGet("/api/recipes/{recipeId}")]
public sealed class GetRecipeEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("getRecipe")
        .Produces<GetRecipeResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int recipeId)
    {
        var userId = currentUser.GetId();
        var query = new GetRecipeQuery(recipeId, userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(GetRecipeMapper.ToDto);
    }
}
