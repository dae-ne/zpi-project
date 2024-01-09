using System.Collections.Generic;

namespace Dietly.WebApi.Resources.Lists.GetList;

public sealed class GetListResponse
{
    public int Count { get; set; }

    public IEnumerable<string> Ingredients { get; set; } = Enumerable.Empty<string>();
}
