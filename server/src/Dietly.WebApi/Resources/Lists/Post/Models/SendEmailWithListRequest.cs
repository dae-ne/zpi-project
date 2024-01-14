using System.Collections.Generic;

namespace Dietly.WebApi.Resources.Lists.Post.Models;

public sealed class SendEmailWithListRequest
{
    public IEnumerable<int> IngredientIds { get; init; } = Enumerable.Empty<int>();
}
