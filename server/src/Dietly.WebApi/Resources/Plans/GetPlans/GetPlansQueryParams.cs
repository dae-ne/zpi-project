using Microsoft.AspNetCore.Mvc;

namespace Dietly.WebApi.Resources.Plans.GetPlans;

public sealed class GetPlansQueryParams
{
    [FromQuery(Name = "from")]
    public string? From { get; init; }

    [FromQuery(Name = "to")]
    public string? To { get; init; }
}
