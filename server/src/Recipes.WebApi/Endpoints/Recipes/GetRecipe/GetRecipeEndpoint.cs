using Recipes.Application.Recipes.Queries.GetRecipe;

namespace Recipes.WebApi.Endpoints.Recipes.GetRecipe;

[ApiEndpointGet("/api/recipes/{recipeId}")]
public sealed class GetRecipeEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
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
        var recipe = await mediator.Send(query);
        var dto = recipe.ToDto();
        return Results.Ok(dto);
    }
}
