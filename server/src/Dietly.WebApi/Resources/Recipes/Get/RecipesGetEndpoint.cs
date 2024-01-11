using Dietly.Application.Recipes.Queries.GetRecipes;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Recipes.Get.Models;

namespace Dietly.WebApi.Resources.Recipes.Get;

public sealed class RecipesGetEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/api/recipes")
        .WithTags("Recipes")
        .WithName("getRecipes")
        .Produces<RecipesGetResponse>();

    public async Task<IResult> HandleAsync()
    {
        var userId = currentUser.GetId();
        var query = new GetRecipesQuery(userId);
        var result = await mediator.Send(query);
        return result.Match(recipes => Results.Ok(recipes.ToDto()), HandleError);
    }
}
