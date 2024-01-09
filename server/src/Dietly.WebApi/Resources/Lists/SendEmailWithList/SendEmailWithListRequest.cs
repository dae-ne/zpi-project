using System.Collections.Generic;

namespace Dietly.WebApi.Resources.Lists.SendEmailWithList;

public sealed class SendEmailWithListRequest
{
    public int UserId { get; init; }

    public IEnumerable<string> Ingredients { get; init; } = Enumerable.Empty<string>();
}
