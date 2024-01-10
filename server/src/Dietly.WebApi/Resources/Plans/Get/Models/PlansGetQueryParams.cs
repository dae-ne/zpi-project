using Microsoft.AspNetCore.Mvc;

namespace Dietly.WebApi.Resources.Plans.Get.Models;

public sealed class PlansGetQueryParams
{
    [FromQuery(Name = "from")]
    public string? From { get; init; }

    [FromQuery(Name = "to")]
    public string? To { get; init; }
}
