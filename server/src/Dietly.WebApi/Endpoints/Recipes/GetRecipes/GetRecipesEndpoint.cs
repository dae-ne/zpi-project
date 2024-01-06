using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;
using Dietly.WebApi.Services;
using Dietly.Application.Recipes.Queries.GetRecipes;

namespace Dietly.WebApi.Endpoints.Recipes.GetRecipes;

[ApiEndpointGet("/api/recipes")]
public sealed class GetRecipesEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
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
        var recipes = await mediator.Send(query);
        var dto = recipes.ToDto();
        return Results.Ok(dto);
    }
}
