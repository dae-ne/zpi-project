using System.Collections.Generic;
using Recipes.WebApi.Endpoints.Recipes.GetRecipe;

namespace Recipes.WebApi.Endpoints.Recipes.GetRecipes;

public sealed class GetRecipesResponse
{
    public int Count { get; init; }
    
    public IEnumerable<GetRecipeResponse> Data { get; init; } = Enumerable.Empty<GetRecipeResponse>();
}
