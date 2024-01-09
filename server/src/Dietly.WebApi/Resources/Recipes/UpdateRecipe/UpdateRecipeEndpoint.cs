using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Recipes.UpdateRecipe;

[ApiEndpointPut("/api/recipes/{recipeId}")]
public sealed class UpdateRecipeEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("updateRecipe");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int recipeId, UpdateRecipeRequest request, HttpContext httpContext)
    {
        var userId = currentUser.GetId();

        if (request.Id != recipeId)
        {
            return Results.BadRequest(new ErrorDto(400, ["Wrong recipe id"]));
        }

        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
