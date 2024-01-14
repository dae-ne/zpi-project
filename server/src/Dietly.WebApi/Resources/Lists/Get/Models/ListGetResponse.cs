using System.Collections.Generic;

namespace Dietly.WebApi.Resources.Lists.Get.Models;

public sealed class ListGetResponse
{
    public int Count { get; set; }

    public IEnumerable<ListGetIngredientDto> Ingredients { get; set; } = Enumerable.Empty<ListGetIngredientDto>();

    public record ListGetIngredientDto(int Id, string Name);
}
