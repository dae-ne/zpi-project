using Microsoft.AspNetCore.Http;

namespace Recipes.Api.Endpoints.Recipes.GetRecipes;

internal sealed class GetRecipesEndpoint : Endpoint<GetRecipesRequest, GetRecipesResponse>
{
    public override void Configure()
    {
        Get("/recipes");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(GetRecipesRequest request, CancellationToken cancellationToken)
    {
        var response = new GetRecipesResponse();
        await SendAsync(response, StatusCodes.Status200OK, cancellationToken);
    }
}
