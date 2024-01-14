using System.Collections.Generic;

namespace Dietly.WebApi.Resources.Recipes.Get.Models;

public sealed class RecipesGetResponse
{
    public int Count { get; init; }

    public IEnumerable<RecipeGetResponse> Data { get; init; } = Enumerable.Empty<RecipeGetResponse>();
}
