using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;
using Dietly.Application.Recipes.Commands.RemoveRecipe;

namespace Dietly.WebApi.Endpoints.Recipes.RemoveRecipe;

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
