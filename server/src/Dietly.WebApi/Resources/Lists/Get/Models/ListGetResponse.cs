using System.Collections.Generic;

namespace Dietly.WebApi.Resources.Lists.Get.Models;

public sealed class ListGetResponse
{
    public int Count { get; set; }

    public IEnumerable<string> Ingredients { get; set; } = Enumerable.Empty<string>();
}
