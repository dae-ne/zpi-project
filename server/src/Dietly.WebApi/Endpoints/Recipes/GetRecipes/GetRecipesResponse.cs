using System.Collections.Generic;
using Dietly.WebApi.Endpoints.Recipes.GetRecipe;

namespace Dietly.WebApi.Endpoints.Recipes.GetRecipes;

public sealed class GetRecipesResponse
{
    public int Count { get; init; }

    public IEnumerable<GetRecipeResponse> Data { get; init; } = Enumerable.Empty<GetRecipeResponse>();
}
