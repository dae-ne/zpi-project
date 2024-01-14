using Dietly.Application.Recipes.Commands.RemoveRecipe;
using Dietly.WebApi.Infrastructure.ApiEndpoints;

namespace Dietly.WebApi.Resources.Recipes.Delete;

public class RecipeDeleteEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Delete("/api/recipes/{recipeId}")
        .WithTags("Recipes")
        .WithName("removeRecipe")
        .Produces(200);

    public async Task<IResult> HandleAsync(int recipeId)
    {
        var userId = currentUser.GetId();
        var command = new RemoveRecipeCommand(RecipeId: recipeId, UserId: userId);
        var result = await mediator.Send(command);
        return result.Match(Results.Ok, HandleError);
    }
}
