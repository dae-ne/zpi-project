using Dietly.Application.Recipes.Commands.RemoveRecipe;

namespace Dietly.WebApi.Resources.Recipes.RemoveRecipe;

[ApiEndpointDelete("/api/recipes/{recipeId}")]
public class RemoveRecipeEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("removeRecipe");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int recipeId)
    {
        var userId = currentUser.GetId();
        var command = new RemoveRecipeCommand(RecipeId: recipeId, UserId: userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
