using Recipes.Application.Recipes.Commands.RemoveRecipe;

namespace Recipes.WebApi.Endpoints.Recipes.RemoveRecipe;

[ApiEndpointDelete("/api/recipes/{recipeId}")]
public class RemoveRecipeEndpoint(IMediator mediator) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("removeRecipe");
    
    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int recipeId)
    {
        var command = new RemoveRecipeCommand(recipeId);
        await mediator.Send(command);
        return Results.Ok();
    }
}
