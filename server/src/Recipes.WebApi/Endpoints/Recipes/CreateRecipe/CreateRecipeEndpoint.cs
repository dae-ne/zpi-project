namespace Recipes.WebApi.Endpoints.Recipes.CreateRecipe;

[ApiEndpointPost("/api/recipes")]
public sealed class CreateRecipeEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Recipes")
        .WithName("createRecipe")
        .Produces(201);
    
    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(CreateRecipeRequest request, HttpContext httpContext)
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(userId);
        var recipeId = await mediator.Send(command);
        return Results.Created();
    }
}