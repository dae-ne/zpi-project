using System.Collections.Generic;
using Recipes.Domain.Entities;
using Recipes.WebApi.Endpoints.Recipes.GetRecipe;

namespace Recipes.WebApi.Endpoints.Recipes.GetRecipes;

internal static class GetRecipesMapper
{
    public static GetRecipesResponse ToDto(this IList<Recipe> recipes) => new()
    {
        Count = recipes.Count,
        Data = recipes.Select(r => r.ToDto())
    };
}
