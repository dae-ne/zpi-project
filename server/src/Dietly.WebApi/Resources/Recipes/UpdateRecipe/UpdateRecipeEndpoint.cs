namespace Dietly.WebApi.Resources.Recipes.UpdateRecipe;

[ApiEndpointPut("/api/recipes/{recipeId}")]
public sealed class UpdateRecipeEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("updateRecipe")
        .Produces(200);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int recipeId, UpdateRecipeRequest request, HttpContext httpContext)
    {
        var userId = currentUser.GetId();

        if (request.Id != recipeId)
        {
            return Results.Problem(statusCode: 400, detail: "The recipe ID provided in the URL does not match the one provided in the request body");
        }

        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
