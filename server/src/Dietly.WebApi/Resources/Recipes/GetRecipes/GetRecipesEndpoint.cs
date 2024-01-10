using Dietly.Application.Recipes.Queries.GetRecipes;

namespace Dietly.WebApi.Resources.Recipes.GetRecipes;

[ApiEndpointGet("/api/recipes")]
public sealed class GetRecipesEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("getRecipes")
        .Produces<GetRecipesResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync()
    {
        var userId = currentUser.GetId();
        var query = new GetRecipesQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(GetRecipesMapper.ToDto);
    }
}
