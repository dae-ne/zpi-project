using System.Collections.Generic;
using Dietly.Domain.Entities;
using Dietly.WebApi.Resources.Recipes.GetRecipe;

namespace Dietly.WebApi.Resources.Recipes.GetRecipes;

internal static class GetRecipesMapper
{
    public static GetRecipesResponse ToDto(this IList<Recipe> recipes) => new()
    {
        Count = recipes.Count,
        Data = recipes.Select(r => r.ToDto())
    };
}
