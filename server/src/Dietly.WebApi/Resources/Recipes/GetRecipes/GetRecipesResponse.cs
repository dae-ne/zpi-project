using System.Collections.Generic;
using Dietly.WebApi.Resources.Recipes.GetRecipe;

namespace Dietly.WebApi.Resources.Recipes.GetRecipes;

public sealed class GetRecipesResponse
{
    public int Count { get; init; }

    public IEnumerable<GetRecipeResponse> Data { get; init; } = Enumerable.Empty<GetRecipeResponse>();
}
