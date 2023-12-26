using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Endpoints.Recipes.UpdateRecipe;

[ApiEndpointPut("/api/recipes/{recipeId}")]
public sealed class CreateRecipeEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("updateRecipe");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int recipeId, UpdateRecipeRequest request, HttpContext httpContext)
    {
        var userId = currentUser.GetId();

        // TODO: request.Id should be equal to recipeId

        var command = request.ToCommand(userId);
        await mediator.Send(command);
        return Results.Ok();
    }
}
